//using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.DB;
//using WebApp.TagHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(opts =>
//{
//    opts.Cookie.IsEssential = true;
//});

//builder.Services.Configure<RazorPagesOptions>(opts => opts.Conventions.AddPageRoute("/Index","/extra/page/{id:long?}"));

builder.Services.AddSingleton<CitiesDataContext>();
builder.Services.Configure<AntiforgeryOptions>(opts =>
{
    opts.HeaderName = "X-XSRF-TOKEN";
});

builder.Services.Configure<MvcOptions>(opts => opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Please enter a value"));

//builder.Services.AddTransient<ITagHelperComponent, TimeTagHelperComponent>();
//builder.Services.AddTransient<ITagHelperComponent, TableFooterSTagHelperComponent>();

var app = builder.Build();

app.UseStaticFiles();
IAntiforgery antiforgery = app.Services.GetRequiredService<IAntiforgery>();

app.Use(async (context, next) =>
{
    if (!context.Request.Path.StartsWithSegments("/api/"))
    {
        string? token = antiforgery.GetAndStoreTokens(context).RequestToken;

        if (token != null)
        {
            context.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
        }
    }

    await next();
});

//app.UseSession();
//app.MapControllers();
//app.MapDefaultControllerRoute();
app.MapControllerRoute("forms", "controllers/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//to default vs MVC framework Views
//app.MapRazorPages().Add(b => ((RouteEndpointBuilder)b).Order = 2);

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();

SeedData.SeedDatabase(context);

app.Run();