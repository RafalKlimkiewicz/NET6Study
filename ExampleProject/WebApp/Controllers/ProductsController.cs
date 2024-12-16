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

        [HttpPost]
        public void SaveProduct([FromBody] Product product)
        {
            _context.Products.Add(product);

            _context.SaveChanges();
        }

        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            _context.Products.Update(product);

            _context.SaveChanges();
        }

        [HttpDelete("id")]
        public void DeleteProduct([FromBody] long id)
        {
            _context.Products.Remove(new Product { ProductId = id });

            _context.SaveChanges();
        }
    }
}