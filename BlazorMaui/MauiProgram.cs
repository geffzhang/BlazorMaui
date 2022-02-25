using BlazorShared.Pages;
using Microsoft.AspNetCore.Components.WebView.Maui;
using System.Globalization;

namespace BlazorMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder
                .RegisterBlazorMauiWebView()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddBlazorWebView();
            builder.Services.AddSharedExtensions();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddFreeSql(option =>
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
            return builder.Build();
        }
    }
}
