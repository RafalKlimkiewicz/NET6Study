using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class MessageAttribute : Attribute, IAsyncAlwaysRunResultFilter, IOrderedFilter
    {
        private int counter = 0;
        private string msg;

        public MessageAttribute(string mesage) => msg = mesage;

        public int Order { get; set; }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Dictionary<string, string> resultData;

            if (context.Result is ViewResult vr && vr.ViewData.Model is Dictionary<string, string> data)
            {
                resultData = data;
            }
            else
            {
                resultData = new Dictionary<string, string>();

                context.Result = new ViewResult
                {
                    ViewName = "/Views/Shared/Message.cshtml",
                    ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = resultData
                    }
                };
            }

            while (resultData.ContainsKey($"Message_{counter}"))
            {
                counter++;
            }

            resultData[$"Message_{counter}"] = msg;

            await next();
        }
    }
}
