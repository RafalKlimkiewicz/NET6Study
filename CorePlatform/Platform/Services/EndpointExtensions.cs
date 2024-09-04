using Platform.Classes;

namespace Platform.Services
{
    public static class EndpointExtensions
    {
        public static void MapWeather(this IEndpointRouteBuilder app, string path)
        {
            var fromatter = app.ServiceProvider.GetRequiredService<IResponseFormatter>();

            app.MapGet(path, context => WeatherEndpoint.Endpoint(context, fromatter));
        }
    }
}