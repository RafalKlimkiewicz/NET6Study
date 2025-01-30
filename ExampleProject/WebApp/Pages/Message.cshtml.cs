using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class MessageModel : PageModel
    {
        public object Message { get; set; } = "This is the Message Razor Page";

        public void OnGet()
        {
        }
    }
}
