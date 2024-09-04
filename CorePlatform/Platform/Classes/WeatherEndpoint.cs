using Platform.Services;

namespace Platform.Classes
{
    public class WeatherEndpoint
    {
        private readonly IResponseFormatter _formatter;

        public WeatherEndpoint(IResponseFormatter formatter)
        {
            _formatter = formatter;
        }

        public async Task Endpoint(HttpContext context)
        {
            await _formatter.Format(context, "Endpoint Class: It is couldy in Milan");

            //await context.Response.WriteAsync("Endpoint Class: It is couldy in Milan");
        }
    }
}