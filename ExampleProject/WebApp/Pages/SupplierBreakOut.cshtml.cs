using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    public class SupplierBreakOutModel : PageModel
    {
        private DataContext _context;

        public SupplierBreakOutModel(DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Supplier? Supplier { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ReturnPage { get; set; }
        public string? ProductId { get; set; }

        public void OnGet([FromQuery(Name = "Product")] Product product, string returnPage)
        {
            TempData["product"] = Serialize(product);
            TempData["returnAction"] = ReturnPage = returnPage;
            TempData["productId"] = ProductId = product.ProductId.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && Supplier != null)
            {
                _context.Suppliers.Add(Supplier);

                await _context.SaveChangesAsync();

                var product = Deserialize(TempData["product"] as string);

                if (product != null)
                {
                    product.SupplierId = Supplier.SupplierId;
                    TempData["product"] = Serialize(product);

                    var id = TempData["productId"] as string;

                    return RedirectToPage(TempData["returnAction"] as string, new { id });
                }
            }

            return Page();
        }

        private string Serialize(Product product) => JsonSerializer.Serialize(product);

        private Product? Deserialize(string? json) => json == null ? null : JsonSerializer.Deserialize<Product>(json);
    }
}
