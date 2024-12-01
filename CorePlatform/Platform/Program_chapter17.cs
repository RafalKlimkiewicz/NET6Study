using Platform.Endpoints;
using Platform.Services;

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

var app = builder.Build();

app.MapEndpoint<SumEndpoint>("/sum/{count:int=1000000000}");

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.Run();