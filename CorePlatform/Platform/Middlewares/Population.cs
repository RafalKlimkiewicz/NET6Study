namespace Platform.Middlewares
{
    public partial class Population
    {
        //private RequestDelegate? _next;

        //public Population() { }

        //public Population(RequestDelegate? next) { _next = next; }

        public static async Task Endpoint(HttpContext context, ILogger<Population> logger)
        {
            //logger?.LogDebug($"Started processing for {context.Request.Path}");
            StartingResponse(logger, context.Request.Path);

            string? city = context.Request.RouteValues["city"] as string ?? "london";
            int? pop = null;

            switch (city.ToLower().ToLower())
            {
                case "london":
                    pop = 8_136_000;
                    break;
                case "paris":
                    pop = 2_141_000;
                    break;
                case "monaco":
                    pop = 39_000;
                    break;
            }

            if (pop.HasValue)
                await context.Response.WriteAsync($"City: {city}, Population: {pop}");
            else
                context.Response.StatusCode = StatusCodes.Status404NotFound;

            logger?.LogDebug($"Finished processing for {context.Request.Path}");
        }

        [LoggerMessage(0, LogLevel.Debug, "Starting resposne for {path}")]
        public static partial void StartingResponse(ILogger logger, string path);

        //public async Task Invoke(HttpContext context)
        //{
        //    string[] parts = context.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);

        //    if (parts.Length == 2 && parts[0] == "population")
        //    {
        //        string city = parts[1];
        //        int? pop = null;

        //        switch (city.ToLower())
        //        {
        //            case "london":
        //                pop = 8_136_000;
        //                break;
        //            case "paris":
        //                pop = 2_141_000;
        //                break;
        //            case "monaco":
        //                pop = 39_000;
        //                break;
        //        }

        //        if (pop.HasValue)
        //        {
        //            await context.Response.WriteAsync($"City: {city}, Population: {pop}");

        //            return;
        //        }
        //    }

        //    if (_next != null)
        //        await _next(context);
        //}
    }
}
