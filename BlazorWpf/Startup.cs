using BlazorShared.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorWpf
{
    public static class Startup
    {
        public static IServiceProvider? Services { get; private set; }

        public static void Init()
        {
            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices(WireupServices)
                           .Build();
            Services = host.Services;
        }

        private static void WireupServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddBlazorWebView();
            services.AddSharedExtensions();
            services.AddSingleton<WeatherForecastService>();
            services.AddFreeSql(option =>
            {
                option.UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=test.db;")
#if DEBUG
                     //开发环境:自动同步实体
                     .UseAutoSyncStructure(true)
                     .UseNoneCommandParameter(true)
                     //调试sql语句输出
                     .UseMonitorCommand(cmd => System.Console.WriteLine(cmd.CommandText))
#endif
                    ;
            });
        }
    }
}
