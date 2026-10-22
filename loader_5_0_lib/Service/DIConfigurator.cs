using Confluent.Kafka;
using loader_5_0_lib.Adapters;
using loader_5_0_lib.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace loader_5_0_lib.Service
{
    public static partial class DIConfigurator
    {
        /// <summary>
        /// Расширение для конфигурирования сервисов
        /// </summary>
        /// <param name="services">Сервисы</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection DIAddServices(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection DIAddAdapters(this IServiceCollection services, IConfiguration config)
        {
            var kafkaOptions = config.GetSection("Kafka").Get<KafkaOptions>() ?? new() { Active = false };
            services.AddSingleton(Options.Create(kafkaOptions));

            services.AddScoped<IKafkaAdapter, KafkaAdapter>();

            services.AddSingleton(sp =>
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = kafkaOptions.BootstrapServers,
                    MessageMaxBytes = kafkaOptions.MessageMaxBytes > 0 ? kafkaOptions.MessageMaxBytes : null,

                    // Задержка в мс между отправкой пакетов сообщений, нужно для равномерного распределения сообщений в партициях
                    LingerMs = 0,

                    // Ожидание сохранения сообщения во всех брокерах кластера, а не только в лидере
                    Acks = Acks.All
                };
                return new ProducerBuilder<string, string>(config).Build();
            });

            return services;
        }
    }
}
