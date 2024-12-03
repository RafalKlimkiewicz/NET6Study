using Microsoft.Extensions.Caching.Distributed;
using Platform.Models;
using Platform.Services;

namespace Platform.Endpoints
{
    public class SumEndpoint
    {
        public async Task Endpoint(HttpContext context, CalculationContext calculationContext)
        {
            int.TryParse((string?)context.Request.RouteValues["count"], out int count);

            long total = calculationContext.Calculations?.FirstOrDefault(c => c.Count == count)?.Result ?? 0;

            if (total == 0)
            {

                for (int i = 0; i <= count; i++)
                    total += i;
            }

            calculationContext.Calculations?.Add(new()
            {
                Count = count,
                Result = total
            });

            await calculationContext.SaveChangesAsync();


            var totalString = $"({DateTime.Now.ToLongTimeString()}) {total}";


            await context.Response.WriteAsync($"<div>({DateTime.Now.ToLongTimeString()}) Total for {count} values: </div><div> {totalString}");
        }
    }
}