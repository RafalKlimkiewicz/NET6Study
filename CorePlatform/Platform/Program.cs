using Microsoft.Extensions.Options;
using Platform;
using Platform.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageOption>(options =>
{
    options.CityName = "Albany";
});

var app = builder.Build();

//app.UseMiddleware<LocationMiddleware>();
//app.UseMiddleware<Population>();
//app.UseMiddleware<Capital>();

app.MapGet("files/{filename}.{ext}", async context =>
{
    await context.Response.WriteAsync("Request was Routed\n");

    foreach (var kvp in context.Request.RouteValues)
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
});

app.MapGet("{first}/{second}/{third}", async context =>
{
    await context.Response.WriteAsync("Request was Routed\n");

    foreach (var route in context.Request.RouteValues)
        await context.Response.WriteAsync($"{route.Key}: {route.Value}\n");
});

app.MapGet("capital/{country=France}", Capital.Endpoint);
app.MapGet("size/{city?}", Population.Endpoint)
    .WithMetadata(new RouteNameMetadata("population"));

//app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("routing", async context =>
//    {
//        await context.Response.WriteAsync("Request was routed");
//    });
//});

//app.MapGet("capital/uk", new Capital().Invoke);
//app.MapGet("population/paris", new Population().Invoke);

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Terminal Middleware Reached");
//});


//app.MapGet("/location", async (HttpContext context, IOptions<MessageOption> msgOpts) =>
//{
//    var opts = msgOpts.Value;

//    await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");
//});

//app.Map("/branch", branch =>
//{
//    branch.UseMiddleware<QueryStringMiddleWare>();

//    branch.Use(async (HttpContext context, Func<Task> next) =>
//    {
//        await context.Response.WriteAsync($"Branch Middelware");
//    });
//});

//app.Map("/branch2", branch =>
//{
//    branch.UseMiddleware<QueryStringMiddleWare>();

//    branch.Run(async (context) =>
//    {
//        await context.Response.WriteAsync($"Branch Middelware");
//    });

//});

//app.Map("/branch3", branch =>
//{
//    branch.Run(new QueryStringMiddleWare().Invoke);
//});

//app.Use(async (context, next) =>
//{
//    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//    {
//        context.Response.ContentType = "text/plain";

//        await context.Response.WriteAsync("Custom Middelware \n");
//    }

//    await next();
//});


//app.Use(async (context, next) =>
//{
//    await next();
//    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
//});

//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/short")
//        await context.Response.WriteAsync("Request short Circuited \n");
//    else
//        await next();
//});

//app.UseMiddleware<QueryStringMiddleWare>();

app.MapGet("/", () => "Hello World!");

app.Run();
