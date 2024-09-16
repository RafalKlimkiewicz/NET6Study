using Platform;

var builder = WebApplication.CreateBuilder(args);

var servicesConfig = builder.Configuration; //set up services
builder.Services.Configure<MessageOption>(servicesConfig.GetSection("Location"));

var servicesEnv = builder.Environment;

var app = builder.Build();

var pipelineConfig = app.Configuration;

var pipelineEnv = app.Environment;

app.MapGet("config", async (HttpContext context, IConfiguration config, IWebHostEnvironment env) =>
{
    var defaultDebug = config["Logging:LogLevel:Default"];

    await context.Response.WriteAsync($"Log level is: {defaultDebug}");

    await context.Response.WriteAsync($"\nThe pipelineEnv is: {pipelineEnv?.EnvironmentName}");
    await context.Response.WriteAsync($"\nThe servicesEnv is: {servicesEnv?.EnvironmentName}");
    await context.Response.WriteAsync($"\nThe env setting is: {env.EnvironmentName}");

    var wsId = config["WebService:Id"];
    var wsKey = config["WebService:Key"];
    
    await context.Response.WriteAsync($"\nwsId: {wsId}");
    await context.Response.WriteAsync($"\nwsKey: {wsKey}");

});


app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    await context.Response.WriteAsync($"Hello World!");
});

app.Run();
