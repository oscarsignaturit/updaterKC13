using updaterKC13.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<VersionService>();

var app = builder.Build();

app.MapControllers();

app.Run();