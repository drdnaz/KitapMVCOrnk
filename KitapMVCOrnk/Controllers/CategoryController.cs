using KitapMVCOrnk.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KitapMVCOrnk.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5079"); // KitapApi'nin çalışan portu
        }
        public async Task<IActionResult> BooksByCategory(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Books");

            if (!response.IsSuccessStatusCode)
                return View(new List<BookViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var allBooks = JsonConvert.DeserializeObject<List<BookViewModel>>(json);

            var filtered = allBooks?.Where(b => b.CategoryId == id).ToList();

            return View(filtered ?? new List<BookViewModel>());
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Category");

            if (!response.IsSuccessStatusCode)
                return View(new List<Category>());

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(json);

            return View(categories ?? new List<Category>());
        }
    }
}