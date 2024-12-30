using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.DB;
using WebApp.Models.Dto;

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

        [HttpGet("object/{format?}")]
        [FormatFilter]
        [Produces("application/json","application/xml")]
        public async Task<ProductBindingTarget?> GetObject()
        {
            var p = await _dataContext.Products.FirstAsync();

            return new ProductBindingTarget()
            {
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId,
            };
        }

        [HttpPost]
        public string SaveProductJson(ProductBindingTarget product)
        {
            return $"JSON: {product.Name}";
        }
        
        //[HttpPost]
        //[Consumes("application/xml")]
        //public string SaveProductXml(ProductBindingTarget product)
        //{
        //    return $"XML: {product.Name}";
        //}
    }
}