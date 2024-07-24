using Platform.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/branch", branch =>
{
    branch.UseMiddleware<QueryStringMiddleWare>();

    branch.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync($"Branch Middelware");
    });
});

app.Map("/branch2", branch =>
{
    branch.UseMiddleware<QueryStringMiddleWare>();

    branch.Run(async (context) =>
    {
        await context.Response.WriteAsync($"Branch Middelware");
    });

});

app.Map("/branch3", branch =>
{
    branch.Run(new QueryStringMiddleWare().Invoke);
});

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";

        await context.Response.WriteAsync("Custom Middelware \n");
    }

    await next();
});


app.Use(async (context, next) =>
{
    await next();
    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/short")
        await context.Response.WriteAsync("Request short Circuited \n");
    else
        await next();
});

app.UseMiddleware<QueryStringMiddleWare>();

app.MapGet("/", () => "Hello World!");

app.Run();
