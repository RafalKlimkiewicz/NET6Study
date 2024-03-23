namespace LanguageFeatures.Models
{
    public class MyAsyncMethod
    {
        public static Task<long?> GetPageLengthTask()
        {
            var client = new HttpClient();

            var httpTask = client.GetAsync("http://apress.com");

            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) =>
                {
                    return antecedent.Result.Content.Headers.ContentLength;
                });
        }

        public static async Task<long?> GetPageLengthAsync()
        {
            var client = new HttpClient();

            var httpMessage = await client.GetAsync("http://apress.com");

            return httpMessage.Content.Headers.ContentLength;
        }

        public static async Task<IEnumerable<long?>> GetPageLengthsAsync(List<string> output, params string[] urls)
        {
            var results = new List<long?>();

            var client = new HttpClient();

            foreach (var url in urls)
            {
                output.Add($"Started request for {url}");

                var httpMessage = await client.GetAsync(url);

                results.Add(httpMessage.Content.Headers.ContentLength);

                output.Add($"Completed request for {url}");
            }

            return results;
        }

        public static async IAsyncEnumerable<long?> GetPageLengthsAsync2(List<string> output, params string[] urls)
        {
            var results = new List<long?>();

            var client = new HttpClient();

            foreach (var url in urls)
            {
                output.Add($"Started request for {url}");

                var httpMessage = await client.GetAsync(url);

                results.Add(httpMessage.Content.Headers.ContentLength);

                output.Add($"Completed request for {url}");

                yield return httpMessage.Content.Headers.ContentLength;
            }
        }
    }
}
