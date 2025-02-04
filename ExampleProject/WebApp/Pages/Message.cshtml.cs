using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Filters;

namespace WebApp.Pages
{
    [RequireHttps]
    //[HttpsOnly]
    //[SimpleCache]
    [SimpleCacheAsync]
    public class MessageModel : PageModel
    {
        public object Message { get; set; } = $"{DateTime.Now.ToLongTimeString()} This is the Message Razor Page";

        public void OnGet()
        {
        }
    }
}