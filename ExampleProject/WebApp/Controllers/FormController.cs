using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private readonly DataContext _context;

        public FormController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index([FromQuery] long? id)
        {
            return View("Form", await _context.Products.FirstOrDefaultAsync(p => id == null || p.ProductId == id));
        }

        [HttpPost]
        public IActionResult SubmitForm(Product product)
        {
            if (ModelState.GetValidationState(nameof(Product.Price)) == ModelValidationState.Valid && product.Price <= 0)
            {
                ModelState.AddModelError(nameof(Product.Price), "Enter a positive price");
            }

            if (ModelState.GetValidationState(nameof(Product.Name)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(Product.Price)) == ModelValidationState.Valid && 
                product.Name.ToLower().StartsWith("small") && product.Price > 100)
            {
                ModelState.AddModelError("", "Small products cannot cost more than $100");
            }

            if (ModelState.GetValidationState(nameof(Product.CategoryId)) == ModelValidationState.Valid 
                && !_context.Categories.Any(c => c.CategoryId == product.CategoryId))
            {
                ModelState.AddModelError(nameof(Product.CategoryId), "Enter an existring categoryId");
            }

            if (ModelState.GetValidationState(nameof(Product.SupplierId)) == ModelValidationState.Valid 
                && !_context.Suppliers.Any(c => c.SupplierId == product.SupplierId))
            {
                ModelState.AddModelError(nameof(Product.SupplierId), "Enter an existring categoryId");
            }

            if (ModelState.IsValid)
            {
                TempData["name"] = product.Name;
                TempData["price"] = product.Price.ToString();
                TempData["categoryId"] = product.CategoryId.ToString();
                TempData["supplierId"] = product.SupplierId.ToString();

                return RedirectToAction(nameof(Results));
            }
            else
            {
                return View("Form");
            }
        }

        public IActionResult Results()
        {
            return View();
        }

        public string Header([FromHeader(Name = "Accept-Language")] string accept)
        {
            return $"Header: {accept}";
        }

        [HttpPost]
        public Product Body([FromBody] Product model)
        {
            return model;
        }
    }
}