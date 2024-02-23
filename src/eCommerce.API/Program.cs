var builder = WebApplication.CreateBuilder(args);

var app = builder.Services.RegisterApi(builder);

app.HostServices();

app.Run();
