var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(opts =>
{
    opts.CheckConsentNeeded = context => true;
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(opts =>
{
    opts.IdleTimeout = TimeSpan.FromMinutes(30);
    opts.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();

app.UseCookiePolicy();

app.UseMiddleware<Platform.Middlewares.ConsentMiddleware>();

app.MapGet("/session", async context =>
{
    int counter1 = (context.Session.GetInt32("counter1") ?? 0) + 1;
    int counter2 = (context.Session.GetInt32("counter2") ?? 0) + 1;

    context.Session.SetInt32("counter1", counter1);
    context.Session.SetInt32("counter2", counter2);

    await context.Session.CommitAsync();
    await context.Response.WriteAsync($"*Counter1: {counter1}, *Counter2: {counter2}");
});

app.MapGet("/cookie", async context =>
{
    int counter1 = int.Parse(context.Request.Cookies["counter1"] ?? "0") + 1;

    context.Response.Cookies.Append("counter1", counter1.ToString(),
        new CookieOptions
        {
            MaxAge = TimeSpan.FromMinutes(30),
            IsEssential = true
        });

    int counter2 = int.Parse(context.Request.Cookies["counter2"] ?? "0") + 1;

    context.Response.Cookies.Append("counter2", counter1.ToString(),
        new CookieOptions
        {
            MaxAge = TimeSpan.FromMinutes(30)
        });

    await context.Response.WriteAsync($"Counter1: {counter1}, Counter2: {counter2}");

});

app.MapGet("clear", async context =>
{
    context.Response.Cookies.Delete("counter1");
    context.Response.Cookies.Delete("counter2");
    context.Response.Redirect("/");

    await Task.CompletedTask;
});

app.MapFallback(async context =>
{
    await context.Response.WriteAsync($"HTTPS Request: {context.Request.IsHttps} \n");
    await context.Response.WriteAsync("Hello World!");
});

app.Run();
