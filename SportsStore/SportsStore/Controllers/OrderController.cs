using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private Cart cart;

        public OrderController() { }

        public ViewResult Checkout() => View(new Order());
    }
}
