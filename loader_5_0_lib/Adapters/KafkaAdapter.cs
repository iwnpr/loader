using Confluent.Kafka;
using Confluent.Kafka.Admin;
using loader_5_0_lib.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using static Confluent.Kafka.ConfigPropertyNames;

namespace loader_5_0_lib.Adapters
{
    public interface IKafkaAdapter
    {
        bool IsAvailable();
        void ProduceToTopic(string topicName, string message, bool compressed);
    }

    public class KafkaAdapter(ILogger<KafkaAdapter> logger, IConfiguration config, IProducer<string, string> producer) : IKafkaAdapter
    {
        private readonly ILogger<KafkaAdapter> _log = logger;
        private IProducer<string, string> _producer = producer;
        private readonly bool _isActive = config.GetSection("Kafka").GetValue("Active", false);
        private readonly string _bootstrapServers = config.GetSection("Kafka").GetValue<string>("BootstrapServers") ?? string.Empty;
        private readonly int _messageMaxBytes = config.GetSection("Kafka").GetValue<int>("MessageMaxBytes");
        private readonly string _groupId = config.GetSection("Kafka").GetValue<string>("GroupId") ?? string.Empty;

        public bool IsAvailable()
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig
            {
                BootstrapServers = _bootstrapServers
            }).Build();

            try
            {
                var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
            }
            catch (Exception e)
            {
                _log.LogCritical("{Message}", e.Message);
                return false;
            }

            return true;
        }

        async Task<Dictionary<string, long>> OffsetAsync(IAdminClient adminClient, List<string> prioriryTopicNames)
        {
            Dictionary<string, long> offsets = new();

            OffsetSpec offsetSpec = OffsetSpec.Earliest();
            offsetSpec = OffsetSpec.Latest();
            int partition = 0;
            IsolationLevel isolationLevel = Enum.Parse<IsolationLevel>("ReadUncommitted");

            List<TopicPartitionOffsetSpec> topicPartitionOffsets = new();

            foreach (var topicName in prioriryTopicNames)
            {
                TopicPartitionOffsetSpec topicPartitionOffsetSpec = new TopicPartitionOffsetSpec()
                {
                    TopicPartition = new TopicPartition(topicName, new Partition(partition)),
                    OffsetSpec = offsetSpec
                };

                topicPartitionOffsets.Add(topicPartitionOffsetSpec);
            }

            var timeout = TimeSpan.FromSeconds(30);
            ListOffsetsOptions options = new ListOffsetsOptions() { RequestTimeout = timeout, IsolationLevel = isolationLevel };

            try
            {
                var listOffsetsResult = await adminClient.ListOffsetsAsync(topicPartitionOffsets, options);
                offsets = listOffsetsResult.ResultInfos.Select(e => new KeyValuePair<string, long>(e.TopicPartitionOffsetError.Topic, e.TopicPartitionOffsetError.Offset.Value)).ToDictionary();
            }
            catch (ListOffsetsException e)
            {
                _log.LogError(e, "Ошибка");
            }
            catch (KafkaException e)
            {
                _log.LogError(e, "Произошла ошибка при перечислении смещений");

                Environment.ExitCode = 1;
            }

            return offsets;
        }

        async Task<Dictionary<string, long>> ConsumerGroupOffsetAsync(IAdminClient adminClient, Dictionary<string, long> priorityTopicOffsets)
        {
            var group = _groupId;
            int partition = 0;

            var tpes = new List<TopicPartition>();
            foreach (var topicOffset in priorityTopicOffsets)
            {
                tpes.Add(new TopicPartition(topicOffset.Key, partition));
            }

            var input = new List<ConsumerGroupTopicPartitions>() { new ConsumerGroupTopicPartitions(group, tpes) };

            try
            {
                var groupOffsets = await adminClient.ListConsumerGroupOffsetsAsync(input);

                foreach (var groupOffset in groupOffsets)
                {
                    if (priorityTopicOffsets.ContainsKey(groupOffset.Partitions.First().Topic))
                    {
                        if (groupOffset.Partitions.First().Offset > 0)
                        {
                            priorityTopicOffsets[groupOffset.Partitions.First().Topic] = priorityTopicOffsets[groupOffset.Partitions.First().Topic] - groupOffset.Partitions.First().Offset;
                        }
                    }
                }

            }
            catch (ListConsumerGroupOffsetsException e)
            {
                _log.LogError("Произошла ошибка при получении списка смещений: {consumerGroupOffsetsReport}", e.Results.Count > 0 ? e.Results[0] : null);
            }
            catch (KafkaException e)
            {
                _log.LogError("Произошла ошибка при получении смещения группы потребителей, код ошибки: {Code}, сообщение об ошибке: {Reason}", e.Error.Code, e.Error.Reason);
            }

            return priorityTopicOffsets;
        }

        public string GetTopicNameMinLag(List<string> priorityTopics)
        {
            Dictionary<string, long> groupLag;

            var adminClientConfig = new AdminClientConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using (var adminClient = new AdminClientBuilder(adminClientConfig).Build())
            {
                Dictionary<string, long> offsets = OffsetAsync(adminClient, priorityTopics).Result;
                groupLag = ConsumerGroupOffsetAsync(adminClient, offsets).Result;
            }
            return groupLag.OrderBy(x => x.Value).First().Key ?? string.Empty;
        }

        private void DeliveryReport(DeliveryReport<string, string> report)
        {
            if (report.Error.IsError || report.Error.IsFatal || report.Error.IsLocalError || report.Error.IsBrokerError)
            {
                _log.LogCritical("Ошибка доставки сообщения: {Reason}", report.Error.Reason);

                throw new KafkaException(report.Error);
            }
            else
            {
                _log.LogDebug("Сообщение доставлено в Топик: {Topic}, Партиция: {Partition}, Смещение: {Offset}", report.Topic, report.Partition, report.Offset);
            }
        }

        public void ProduceToTopic(string topicName, string message, bool compressed)
        {
            Message<string, string> kafkaMessage = GetKafkaMessage(message, compressed);

            _producer.Produce(topicName, kafkaMessage, DeliveryReport);

            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Message<string, string> GetKafkaMessage(string message, bool compressed)
        {
            return new Message<string, string>()
            {
                Key = compressed ? "compressed" : string.Empty,
                Value = message
            };
        }

        public Message<string, string> GetMassage(XElement xMessage, bool compress = true)
        {
            return GetKafkaMessage(xMessage.ToString(), compress);
        }
    }
}
