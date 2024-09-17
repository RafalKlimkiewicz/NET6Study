using Microsoft.Extensions.FileProviders;
using Platform.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(opts =>
{
    opts.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestMethod | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath
    | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode;
});

var app = builder.Build();

app.UseHttpLogging();

var env = app.Environment;

app.UseStaticFiles();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider($"{env.ContentRootPath}/staticfiles"),
    RequestPath = "/files",
});

app.MapGet("population/{city?}", Population.Endpoint);

app.Run();
