using System.Runtime.Serialization;
using Platform.Classes;
using Platform.Middlewares;
using Platform.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<WeatherMiddleware>();

app.MapGet("middleware/function", async (HttpContext context, HtmlResponseFormatter formatter) =>
{
    await formatter.Format(context, "Middleware Function: It is snowing in Chicago");
});

app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);

app.MapGet("endpoint/function", async (HttpContext context, HtmlResponseFormatter formatter) =>
{
    await formatter.Format(context, "Endpoint Function: It is sunny in LA");
});

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();
