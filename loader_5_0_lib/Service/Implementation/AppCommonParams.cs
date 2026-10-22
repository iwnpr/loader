using loader_5_0_lib.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace loader_5_0_lib.Service.Implementation
{
    public class AppCommonParams : IAppCommonParams
    {
        private readonly ILogger<AppCommonParams> _logger;
        private readonly IConfiguration _config;
        public string AppBaseDirectory { get; }
        public string DataTemplateFL { get; }
        public string DataTemplateUL { get; }
        public AppCommonParams(ILogger<AppCommonParams> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;


            DataTemplateFL = config.GetSection("Settings").GetValue<string>("DataTemplateFL") ?? string.Empty;
            DataTemplateUL = config.GetSection("Settings").GetValue<string>("DataTemplateUL") ?? string.Empty;

            AppBaseDirectory = AppContext.BaseDirectory;
        }
    }
}
