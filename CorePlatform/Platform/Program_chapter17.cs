using Platform.Endpoints;
using Platform.Models;
using Platform.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDistributedMemoryCache(opts =>
//{
//    opts.SizeLimit = 200;
//});

builder.Services.AddDistributedSqlServerCache(opts =>
{
    opts.ConnectionString = builder.Configuration["ConnectionStrings:CacheConnection"];
    opts.SchemaName = "dbo";
    opts.TableName = "DataCache";
});

builder.Services.AddResponseCaching();
builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

builder.Services.AddDbContext<CalculationContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:CalcConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddTransient<SeedData>();

var app = builder.Build();

app.UseResponseCaching();

app.MapEndpoint<SumEndpoint>("/sum/{count:int=1000000000}");

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

bool cmLineInit = (app.Configuration["INITDB"] ?? "false") == "true";

if(app.Environment.IsDevelopment() || cmLineInit)
{
    using (var scope = app.Services.CreateScope())
    {
        var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
        seedData.SeedDatabase();
    }
}

if (!cmLineInit)
{
    app.Run();
}
