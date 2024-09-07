using Platform.Services;

namespace Platform.Middlewares
{
    public class WeatherMiddleware
    {
        private readonly RequestDelegate _next;

        public WeatherMiddleware(RequestDelegate nextDelegate)
        {
            _next = nextDelegate;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter1, IResponseFormatter formatter2, IResponseFormatter formatter3)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await formatter1.Format(context, "");
                await formatter2.Format(context, "");
                await formatter3.Format(context, "");
            }
            else
                await _next(context);
        }
    }
}