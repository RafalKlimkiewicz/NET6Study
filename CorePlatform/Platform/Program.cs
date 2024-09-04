using Platform.Classes;
using Platform.Middlewares;
using Platform.Services;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.UseMiddleware<WeatherMiddleware>();

//IResponseFormatter formatter = new TextResponseFormatter();

app.MapGet("middleware/function", async (context) =>
{
    await TextResponseFormatter.Singleton.Format(context, "Middleware Function: It is snowing in Chicago");
});

app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);

app.MapGet("endpoint/function", async (context) =>
{
    await TextResponseFormatter.Singleton.Format(context, "Endpoint Function: It is sunny in LA");
});

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();
