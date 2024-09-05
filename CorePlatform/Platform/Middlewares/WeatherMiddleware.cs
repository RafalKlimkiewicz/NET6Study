using Platform.Services;

namespace Platform.Middlewares
{
    public class WeatherMiddleware
    {
        private readonly RequestDelegate _next;
       //private readonly IResponseFormatter _responseFormatter;

        public WeatherMiddleware(RequestDelegate nextDelegate, IResponseFormatter responseFormatter)
        {
            _next = nextDelegate;
            //_responseFormatter = responseFormatter;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter responseFormatter)
        {
            if (context.Request.Path == "/middleware/class")
                await responseFormatter.Format(context, "Middleware Class: It is raining in London");
            else
                await _next(context);
        }
    }
}