using Microsoft.Extensions.Options;

namespace Platform.Middlewares
{
    public class LocationMiddleware
    {
        private RequestDelegate _next;
        private MessageOption _options;
             
        public LocationMiddleware(RequestDelegate next, IOptions<MessageOption> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path == "/location")
                await context.Response.WriteAsync($"{_options.CityName}, {_options.CountryName}\n");
                
            await _next(context);
        }
    }
}
