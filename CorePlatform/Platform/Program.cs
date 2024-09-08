using Platform.Classes;
using Platform.Middlewares;
using Platform.Services;
using Platform.Services.ChainDependency;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var config = builder.Configuration;

if (env.IsDevelopment())
{
    //builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
    //builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();

    builder.Services.AddScoped<IResponseFormatter>(serviceProvider =>
    {
        string? typeName = config["services:IResponseFormatter"];

        return (IResponseFormatter)ActivatorUtilities.CreateInstance(serviceProvider, typeName == null ? typeof(GuidService) : Type.GetType(typeName, true)!);
    });
}
else
{
    builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
}

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<WeatherMiddleware>();
app.MapEndpoint<WeatherEndpoint>("endpoint/class");

app.MapGet("middleware/function", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Middleware Function: It is snowing in Chicago");
});

//app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);
//app.MapWeather("endpoint/class");

app.MapGet("endpoint/function", async (HttpContext context) =>
{
    IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();

    await formatter.Format(context, "Endpoint Function: It is sunny in LA");
});

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();
