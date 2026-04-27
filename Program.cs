using Microsoft.Extensions.FileProviders;
using updaterKC13.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<VersionService>();

var app = builder.Build();

app.MapControllers();

var downloadsPath = Path.Combine(AppContext.BaseDirectory, "downloads");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(downloadsPath),
    RequestPath = "/downloads"
});

app.Run();