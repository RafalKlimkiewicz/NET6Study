namespace Platform.Middlewares
{
    public class Capital
    {
        //private RequestDelegate? _next;

        //public Capital() { }

        //public Capital(RequestDelegate? next) { _next = next; }

        public static async Task Endpoint(HttpContext context)
        {
            string? capital = null;
            string? country = context.Request.RouteValues["country"] as string;

            switch ((country ?? "").ToLower())
            {
                case "uk":
                    capital = "London";
                    break;
                case "france":
                    capital = "Paris";
                    break;
                case "monaco":
                    LinkGenerator? generator = context.RequestServices.GetService<LinkGenerator>();

                    string? url = generator?.GetPathByRouteValues(context, "pupulation", new { city = country });

                    if (url != null)
                        context.Response.Redirect(url);

                    return;
            }

            if (capital != null)
                await context.Response.WriteAsync($"{capital} is capital of {country}");
            else
                context.Response.StatusCode = StatusCodes.Status404NotFound;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    string[] parts = context.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);

        //    if (parts.Length == 2 && parts[0] == "capital")
        //    {
        //        string country = parts[1];
        //        string? capital = null;

        //        switch (country.ToLower())
        //        {
        //            case "uk":
        //                capital = "London";
        //                break;
        //            case "france":
        //                capital = "Paris";
        //                break;
        //            case "monaco":
        //                capital = "";
        //                context.Response.Redirect($"/population/{country}");
        //                break;
        //        }

        //        if (capital != null)
        //        {
        //            await context.Response.WriteAsync($"{capital} is capital of {country}");

        //            return;
        //        }
        //    }

        //    if (_next != null)
        //        await _next(context);
        //}
    }
}
