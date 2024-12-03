using Microsoft.Extensions.Caching.Distributed;
using Platform.Services;

namespace Platform.Endpoints
{
    public class SumEndpoint
    {
        public async Task Endpoint(HttpContext context, IDistributedCache cache, IResponseFormatter formatter, LinkGenerator generator)
        {
            int.TryParse((string?)context.Request.RouteValues["count"], out int count);

            long total = 0;
            
            for (int i = 0; i <= count; i++)
                total += i;

            var totalString = $"({DateTime.Now.ToLongTimeString()}) {total}";

            context.Response.Headers["Cache-Control"] = "public, max-age=120";

            string? url = generator.GetPathByRouteValues(context, null, new { count });

            await formatter.Format(context, $"<div>({DateTime.Now.ToLongTimeString()}) Total for {count}" +
                $" values: </div><div> {totalString}" +
                $"</div> <a href={url}>Reload</a>");
        }
    }
}