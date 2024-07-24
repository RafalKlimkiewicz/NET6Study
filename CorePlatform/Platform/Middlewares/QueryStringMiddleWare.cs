namespace Platform.Middlewares
{
    public class QueryStringMiddleWare
    {
        private RequestDelegate? _next;

        public QueryStringMiddleWare() { }

        public QueryStringMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                if (!context.Response.HasStarted)
                    context.Response.ContentType = "text/plain";

                if (_next == null)
                    await context.Response.WriteAsync("Class-based Middleware with null \n");
                else
                    await context.Response.WriteAsync("Class-based Middleware \n");

            }

            if (_next != null)
                await _next(context);
        }
    }
}
