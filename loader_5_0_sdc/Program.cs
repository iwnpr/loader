using loader_5_0_lib.Classes;
using loader_5_0_lib.DataBase;
using loader_5_0_lib.Service;
using loader_5_0_lib.Service.Implementation;
using loader_5_0_lib.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace loader_5_0_sdc
{
    internal class Program
    {
        public static IConfiguration Configuration;
        public static IServiceProvider ServiceProvider;
        public static readonly int ProcessId = Process.GetCurrentProcess().Id;
        public static readonly string EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"; //appsettings.Development.json
        public static readonly string? ServiceName = Process.GetCurrentProcess().MainModule?.ModuleName;
        public static readonly string? ServiceVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        static void Main(string[] args)
        {
            Initialize();
            ServiceProvider = ConfigureServices();
            ILogger<Program> _log = ServiceProvider.GetRequiredService<ILogger<Program>>();
            ILoader Loader = ServiceProvider.GetRequiredService<ILoader>();

            Loader.StartUploading();
        }
        private static void Initialize()
        {
            // Регистрация провайдера для возможности работать c кодировкой windows-1251
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string CultureName = Thread.CurrentThread.CurrentCulture.Name;
            CultureInfo ci = new CultureInfo(CultureName);
            if (ci.NumberFormat.NumberDecimalSeparator != ".")
            {
                // Forcing use of decimal separator for numerical values
                ci.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = ci;
            }

            // Настройка конфигурации
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{EnvironmentName}.json")
                .AddEnvironmentVariables()
                .Build();
        }

        private static IServiceProvider ConfigureServices()
        {
            // Создание коллекции используемых сервисов
            var services = new ServiceCollection();

            // Регистрация конфигурации
            services.AddSingleton(Configuration);

            #region Serilog

            services.AddLogging(builder => builder.AddSerilog(new LoggerConfiguration()
                .Enrich.WithDynamicProperty("key_id",() => AppContextParams.GetSomething())
                //.Enrich.WithProperty(nameof(LaunchingId), LaunchingId)
                .ReadFrom.Configuration(Configuration)
                .CreateLogger()));

            #endregion Serilog

            services.AddDbContext<MSSQLDbContext>(options =>
            {
                options
                    .UseSqlServer(Configuration.GetConnectionString("BureauWebUi"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddSingleton<ILoader, Loader>();
            services.AddSingleton<IAppParam, AppParam>();
            services.AddSingleton<IStopWatch, StopWatch>();
            services.AddSingleton<IAppXNames, AppXNames>();
            services.AddSingleton<IBureauWebUi, BureauWebUi>();
            services.AddSingleton<IBureauData, BureauData>();
            services.AddSingleton<IAppCommonParams, AppCommonParams>();
            services.AddSingleton<IDocService, DocService>();
            services.AddSingleton<IUniDocService, UniDocService>();
            services.AddSingleton<IDbService, DbService>();

            services.DIAddAdapters(Configuration).DIAddServices();

            return services.BuildServiceProvider();
        }
    }
}
