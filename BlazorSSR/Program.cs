// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BlazorShared.Pages;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes =
    ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "image/svg+xml" });
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = (CompressionLevel)4;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseStaticFiles();
//app.UseDefaultFiles();
app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    RequestPath = new PathString("/pic")
});
app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
