using KitapMVCOrnk.Models;
using Microsoft.AspNetCore.Mvc;

namespace KitapMVCOrnk.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult AddToCart(int id, string title, decimal price, string imageUrl)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(c => c.BookId == id);
            if (existing != null)
                existing.Quantity++;
            else
                cart.Add(new CartItem { BookId = id, Title = title, Price = price, ImageUrl = imageUrl });

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var itemToRemove = cart.FirstOrDefault(c => c.BookId == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObjectAsJson("Cart", cart);

                // Kullanıcıya bilgi mesajı göster
                TempData["message"] = $"\"{itemToRemove.Title}\" sepetten silindi.";

            }

            return RedirectToAction("Index");
        }

    }
}