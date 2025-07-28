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
            _httpClient.BaseAddress = new Uri("http://localhost:5079"); // API portuna göre ayarla
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1; // Sabit kullanıcı (test için)

            var response = await _httpClient.GetAsync($"/api/Favorite/{userId}");

            if (!response.IsSuccessStatusCode)
                return View(new List<BookViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<BookViewModel>>(json);

            return View(books ?? new List<BookViewModel>());
        }
    }
}