//using Microsoft.AspNetCore.Mvc;
//using LanguageFeatures.Models;
using LanguageFeatures.Extensions;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        bool FilterByPrice(Product? product)
        {
            return (product?.Price ?? 0) >= 20;
        }

        public ViewResult Index()
        {
            Product?[]? products = Product.GetProducts();

            //1
            //var p = products[0];
            //string val;

            //if (p != null)
            //    val = p.Name;
            //else
            //    val = "No value";

            //return View(new string[] { val });

            //2
            //string? val2 = products[0]?.Name;

            //if (val2 != null)
            //    return View(new string[] { val2 });

            //return View(new string[] { "No value" });


            //3 ??
            //return View(new string[] { products[0]?.Name ?? "No value" });

            //4 !
            //return View(new string[] { products[0]!.Name });

            //5 interpolation
            //return View(new string[] { $"Name: {products[0]?.Name}, Price: {products[0]?.Price}" });

            //var data = new object[]
            //{
            //    275M, 29.95M, "apple", "orange", 100, 10
            //};

            //decimal total = 0;

            //6
            //for (int i = 0; i < data.Length; i++)
            //    if (data[i] is decimal d)
            //        total += d;

            //7
            //for (int i = 0; i < data.Length; i++)
            //{
            //    switch (data[i])
            //    {
            //        case decimal decimalValue:
            //            total += decimalValue;
            //            break;
            //        case int intValue when intValue > 50:
            //            total += intValue; 
            //            break;
            //    }
            //}

            //8
            //ShoppingCart cart = new()
            //{
            //    Products = Product.GetProducts()
            //};

            //decimal cartTotal = cart.TotalPrices();

            Product[] productArray =
            {
                new Product { Name= "Kajak", Price = 275M},
                new Product { Name = "LifeJacket", Price = 48.95M},
                new Product { Name = "Soccer ball", Price = 19.50M},
                new Product { Name = "Corner flag", Price = 34.95M}
            };

            Func<Product?, bool> nameFilter = delegate (Product? product)
            {
                return product?.Name?[0] == 'S';
            };

            var products2 = new[]
{
                new  { Name= "Kajak", Price = 275M},
                new  { Name = "LifeJacket", Price = 48.95M},
                new  { Name = "Soccer ball", Price = 19.50M},
                new  { Name = "Corner flag", Price = 34.95M}
            };

            //var cartTotal = cart.TotalPrices();
            var arrayTotal = productArray.FilterByPrice(20).TotalPrices();

            decimal priceFilterTotal = productArray.Filter(FilterByPrice).TotalPrices();
            decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();

            decimal priceFilterTotalLambda = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();
            decimal nameFilterTotalLambda = productArray.Filter(p => p?.Name?[0] == 'S').TotalPrices();

            //return View("Index", products2.Select(p => p.Name));
            //return View("Index", products2.Select(p => p.GetType().Name));

            IProductSelection cart = new ShoppingCart(
                new Product { Name = "Kajak", Price = 275M },
                new Product { Name = "LifeJacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M });

            return View(productArray.Select(p => $"{nameof(p.Name)}: {p.Name}, {nameof(p.Price)}: {p.Price}"));
            return View(cart.Names);

            return View("Index", new string[] {
                GetFormatedString(arrayTotal, nameof(arrayTotal)),
                GetFormatedString(priceFilterTotal, nameof(priceFilterTotal)),
                GetFormatedString(nameFilterTotal, nameof(nameFilterTotal)),
                GetFormatedString(priceFilterTotalLambda, nameof(priceFilterTotalLambda)),
                GetFormatedString(nameFilterTotalLambda, nameof(nameFilterTotalLambda))
            });
        }

        public async Task<ViewResult> Index2()
        {
            long? length = await MyAsyncMethod.GetPageLengthAsync();

            return View("Index", new string[] { $"Length: {length}" });
        }

        public async Task<ViewResult> Index3()
        {
            List<string> output = new();

            foreach (var len in await MyAsyncMethod.GetPageLengthsAsync(output, "http://apress.com", "http://microsoft.com", "http://amazon.com"))
            {
                output.Add($"Page length: {len}");
            }

            return View("Index", output);
        }

        public async Task<ViewResult> Index4()
        {
            List<string> output = new();

            await foreach (var len in MyAsyncMethod.GetPageLengthsAsync2(output, "http://apress.com", "http://microsoft.com", "http://amazon.com"))
                output.Add($"Page length: {len}");

            return View("Index", output);
        }

        private string GetFormatedString(decimal value, string variableName)
        {
            return $"{variableName}: {value:C2}";
        }
    }
}