using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ContentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("string")]
        public string GetString() => "This is a string response";

        [HttpGet("object")]
        public async Task<Product?> GetObject()
        {
            return await _dataContext.Products.FindAsync();
        }
    }
}