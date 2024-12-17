using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private DataContext _dataContext;

        public SuppliersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{id}")]
        public async Task<Supplier?> GetSupplier(long id)
        {
            //return await _dataContext.Suppliers.Include(s => s.Products).FirstAsync(s => s.SupplierId == id);

            var supplier = await _dataContext.Suppliers.Include(s => s.Products).FirstAsync(s => s.SupplierId == id);

            //break circulation error  - "A possible object cycle was detected."
            if (supplier.Products != null)
            {
                foreach (var product in supplier.Products)
                    product.Supplier = null;
            }

            return supplier;

        }

        [HttpPatch("{id}")]
        public async Task<Supplier?> PatchSupplier(long id, JsonPatchDocument<Supplier> pathDoc)
        {
            var s = await _dataContext.Suppliers.FindAsync(id);

            if (s != null)
            {
                pathDoc.ApplyTo(s);
                await _dataContext.SaveChangesAsync();
            }

            return s;
        }
    }
}