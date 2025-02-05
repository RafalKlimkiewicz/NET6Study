using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Filters;

namespace WebApp.Pages
{
    [RequireHttps]
    //[HttpsOnly]
    //[SimpleCache]
    //[SimpleCacheAsync]
    [ChangePageArgs]
    public class MessageModel : PageModel
    {
        public object Message { get; set; } = $"{DateTime.Now.ToLongTimeString()} This is the Message Razor Page";

        public void OnGet(string message1, string message2)
        {
            Message = $"{message1}, {message2}";
        }

        //public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        //{
        //    if (context.HandlerArguments.ContainsKey("message1"))
        //    {
        //        context.HandlerArguments["message1"] = "New message";
        //    }
        //}
    }
}