using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ValidationController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("categorykey")]
        public bool CategoryKey(string? categoryId, [FromQuery] KeyTarget target)
        {
            return long.TryParse(categoryId ?? target.CategoryId, out long keyVal) && _dataContext.Categories.Find(keyVal) != null;
        }


        [HttpGet("supplierkey")]
        public bool SupplierKey(string? supplierId, [FromQuery] KeyTarget target)
        {
            return long.TryParse(supplierId ?? target.SupplierId, out long keyVal) && _dataContext.Suppliers.Find(keyVal) != null;
        }
    }

    [Bind(Prefix = nameof(Product))]
    public class KeyTarget
    {
        public string? CategoryId { get; set; }
        public string? SupplierId { get; set; }
    }
}
