using OrderManager;
using OrderManager.Api.Extension;
using OrderManager.Common;
using OrderManager.Database.Extension;

IHost host = Host.CreateDefaultBuilder(args)
#if DEBUG
    .UseConsoleLifetime()
#else
    .UseWindowsService()
#endif
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        #if DEBUG
        config.SetBasePath(Environment.CurrentDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
        #else
        config.SetBasePath(Environment.CurrentDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        #endif
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddOptions();

        services.Configure<MongoDbSetting>(hostContext.Configuration.GetSection("MongoDb"));
        services.Configure<ApiSetting>(hostContext.Configuration.GetSection("Api"));

        services.AddMongoDb();
        services.AddApi();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
