using WebApp.Models.DB;

namespace WebApp.Middlewares
{
    public class TestMiddleware
    {
        private RequestDelegate _nextDelegate;

        public TestMiddleware(RequestDelegate next)
        {
            _nextDelegate = next;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            if(context.Request.Path == "/test")
            {
                await context.Response.WriteAsync($"There are {dataContext.Products.Count()} {nameof(dataContext.Products)} \n");
                await context.Response.WriteAsync($"There are {dataContext.Categories.Count()} {nameof(dataContext.Categories)} \n");
                await context.Response.WriteAsync($"There are {dataContext.Suppliers.Count()} {nameof(dataContext.Suppliers)} \n");
            }
            else
            {
                await _nextDelegate(context);
            }
        }
    }
}
