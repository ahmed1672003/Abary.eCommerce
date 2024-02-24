var builder = WebApplication.CreateBuilder(args);

var app = await builder.Services.RegisterApiAsync(builder);

app.HostServices();

app.Run();
