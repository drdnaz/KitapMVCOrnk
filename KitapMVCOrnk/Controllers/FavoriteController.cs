using KitapMVCOrnk.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KitapMVCOrnk.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly HttpClient _httpClient;

        public FavoriteController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5079");
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Eğer giriş yapılmamışsa favori gösterme!
                return RedirectToAction("Login", "Home");
            }

            // Tüm favori kayıtlarını çek
            var favResponse = await _httpClient.GetAsync("/api/Favorite");
            if (!favResponse.IsSuccessStatusCode)
                return View(new List<BookViewModel>());

            var favJson = await favResponse.Content.ReadAsStringAsync();
            var favorites = JsonConvert.DeserializeObject<List<FavoriteViewModel>>(favJson);

            // Sadece bu kullanıcıya ait favoriler
            var userFavorites = favorites.Where(f => f.UserId == userId.Value).ToList();

            // Kitap bilgilerini topla
            var books = new List<BookViewModel>();
            foreach (var fav in userFavorites)
            {
                var bookResp = await _httpClient.GetAsync($"/api/Books/{fav.BookId}");
                if (!bookResp.IsSuccessStatusCode) continue;

                var bookJson = await bookResp.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<BookViewModel>(bookJson);
                if (book != null)
                    books.Add(book);
            }

            return View(books);
        }
    }
}
