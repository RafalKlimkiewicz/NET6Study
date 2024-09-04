using Platform.Services;

namespace Platform.Classes
{
    public class WeatherEndpoint
    {
        public static async Task Endpoint(HttpContext context, IResponseFormatter formatter)
        {
            await formatter.Format(context, "Endpoint Class: It is couldy in Milan");

            //await context.Response.WriteAsync("Endpoint Class: It is couldy in Milan");
        }
    }
}