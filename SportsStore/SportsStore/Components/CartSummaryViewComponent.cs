using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartServices)
        {
            cart = cartServices;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
