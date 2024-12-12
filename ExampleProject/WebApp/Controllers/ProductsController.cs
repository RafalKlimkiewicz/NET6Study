using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        [HttpGet("{id}")]
        public Product? GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            logger.LogDebug("GerProduct Action Invoked");

            return _context.Products.Find(id);
        }
    }
}