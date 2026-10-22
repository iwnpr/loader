using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace loader_5_0_lib.Service.Implementation
{
    internal interface ITopicService
    {
        string GetNextPriorityTopicName();
    }
    public class TopicService(ILogger<TopicService> logger, IConfiguration config) : ITopicService
    {
        private readonly Dictionary<int, string> PriorityOrderFileNamePrefix = config.GetSection("Settings").GetSection("PriorityOrder").GetSection("FileNamePrefixs").Get<Dictionary<int, string>>() ?? [];

        public string GetNextPriorityTopicName()
        {
            throw new NotImplementedException();
        }
    }
}
