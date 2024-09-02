using Platform;
using Platform.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageOption>(options =>
{
    options.CityName = "Albany";
});

builder.Services.Configure<RouteOptions>(opts =>
{
    opts.ConstraintMap.Add("countryName", typeof(CountryRouteConstraint));
});


var app = builder.Build();


app.Use(async (context, next) =>
{
    Endpoint? end = context.GetEndpoint();

    if(end != null)
        await context.Response.WriteAsync($"{end.DisplayName} Selected");
    else
        await context.Response.WriteAsync($"No endpoint Selected");

    await next();
});



//ambigous route  -- fix Add Route Endpoint Builder order
app.Map("{number:int}", async context =>
{
    await context.Response.WriteAsync("Routed to the int endpoint");
}).WithDisplayName("INT endpoint")
.Add(b => ((RouteEndpointBuilder)b).Order = 1); //fix

app.Map("{number:double}", async context =>
{
    await context.Response.WriteAsync("Routed to the dobule endpoint");
}).WithDisplayName("Double endpoint")
.Add(b => ((RouteEndpointBuilder)b).Order = 2); //fix

//---

app.UseMiddleware<LocationMiddleware>();
//app.UseMiddleware<Population>();
//app.UseMiddleware<Capital>();

//alpha - from a to -z
app.MapGet("{first:alpha:length(3)}/{second:bool}/{*catchcall}", async context =>
{
    await context.Response.WriteAsync("Request was Routed\n");

    foreach (var kvp in context.Request.RouteValues)
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
});

app.MapGet("{first}/{second}/{*catchcall}", async context =>
{
    await context.Response.WriteAsync("Request was Routed\n");

    foreach (var kvp in context.Request.RouteValues)
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
});


app.MapGet("{first:int}/{second:bool}", async context =>
{
    await context.Response.WriteAsync("Request was Routed\n");

    foreach (var route in context.Request.RouteValues)
        await context.Response.WriteAsync($"{route.Key}: {route.Value}\n");
});

app.MapGet("capital2/{country:countryName}", Capital.Endpoint);

app.MapGet("capital/{country=France}", Capital.Endpoint);

app.MapGet("capital/{country:regex(^uk|france|monaco$)}", Capital.Endpoint);
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

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();
