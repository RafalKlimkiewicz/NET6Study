using System.Collections;
using Platform.Classes;
using Platform.Middlewares;
using Platform.Services;

var builder = WebApplication.CreateBuilder(args);

//MultipleDIServicesImplementation(builder);


UsingUnboundTypesInServices(builder);

var app = builder.Build();

UsingUnboundTypesInServices(app);


//MultipleDIServicesImplementation2(app);

//BuildDIUntil385Page(app);

app.Run();

static void BuildDIUntil385Page(WebApplication app)
{
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
}

static void MultipleDIServicesImplementation(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IResponseFormatter, TextResponseFormatter>();
    builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
    builder.Services.AddScoped<IResponseFormatter, GuidService>();
}

static void MultipleDIServicesImplementation2(WebApplication app)
{
    app.MapGet("single", async context =>
    {
        var formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();

        await formatter.Format(context, "Single Service");
    });

    app.MapGet("/", async context =>
    {
        var formatter = context.RequestServices.GetServices<IResponseFormatter>().First(f => f.RichOutput);

        await formatter.Format(context, "Multiple Services");
    });
}

static void UsingUnboundTypesInServices(WebApplication app)
{
    app.MapGet("string", async context =>
    {
        var collection = context.RequestServices.GetRequiredService<ICollection<string>>();

        collection.Add($"Request: {DateTime.Now.ToLongTimeString()}");

        foreach (var str in collection)
        {
            await context.Response.WriteAsync($"string: {str}\n");
        }
    });

    app.MapGet("int", async context =>
    {
        var collection = context.RequestServices.GetRequiredService<ICollection<int>>();

        collection.Add(collection.Count + 1);

        foreach (var val in collection)
        {
            await context.Response.WriteAsync($"Int: {val}\n");
        }
    });
}

static void UsingUnboundTypesInServices(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton(typeof(ICollection<>), typeof(List<>));
}