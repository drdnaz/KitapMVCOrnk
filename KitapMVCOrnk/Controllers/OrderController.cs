using KitapMVCOrnk.Context;
using KitapMVCOrnk.Models;
using Microsoft.AspNetCore.Mvc;

namespace KitapMVCOrnk.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Complete()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult CompleteOrder(int userId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (cart == null || cart.Count == 0)
            {
                TempData["message"] = "Sepetiniz boş. Sipariş oluşturulamadı.";
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cart)
            {
                order.OrderItems.Add(new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity
                });
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            HttpContext.Session.Remove("Cart"); // sepeti temizle
            TempData["message"] = "Siparişiniz başarıyla oluşturuldu.";
            return RedirectToAction("Index", "Cart");
        }
    }
}
