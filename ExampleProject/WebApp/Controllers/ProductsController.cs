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
            return new[]
            {
                new Product
                {
                    Name = "Product #1",
                },
                new Product
                {
                    Name = "Product #2",
                }

            };
        }

        [HttpGet("{id}")]
        public Product GetProduct(long id)
        {
            return new Product()
            {
                ProductId = id,
                Name = "Test Product",
            };
        }
    }
}