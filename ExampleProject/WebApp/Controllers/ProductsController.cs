using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.DB;
using WebApp.Models.Dto;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return _context.Products.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult?> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            logger.LogDebug("GerProduct Action Invoked");

            var p = await _context.Products.FindAsync(id);

            //supplier null / category null before
            return p == null ? NotFound() : Ok(new
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget target)
        {
            //if (ModelState.IsValid)
            {
                var p = target.ToProduct();

                await _context.Products.AddAsync(p);

                await _context.SaveChangesAsync();

                return Ok(p);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();
        }

        [HttpDelete("id")]
        public async Task DeleteProduct([FromBody] long id)
        {
            _context.Products.Remove(new Product { ProductId = id });

            await _context.SaveChangesAsync();
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            return Redirect("/api/products/1");
        }

        [HttpGet("redirectToProductAction")]
        public IActionResult RedirectToProductAction()
        {
            return RedirectToAction(nameof(GetProduct), new { Id = 1 });
        }

        [HttpGet("redirectToActionPermanent")]
        public IActionResult RedirectToActionPermanent()
        {
            return RedirectToActionPermanent(nameof(GetProduct), new { Id = 2 });
        }

    }
}