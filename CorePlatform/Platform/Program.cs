var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("config", async (HttpContext context, IConfiguration config) =>
{
    var defaultDebug = config["Logging:LogLevel:Default"];

    await context.Response.WriteAsync($"Log level is: {defaultDebug}");
});


app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    await context.Response.WriteAsync($"Hello World!");
});

app.Run();
