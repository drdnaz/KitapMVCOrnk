using KitapMVCOrnk.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KitapMVCOrnk.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5079");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Books/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<BookViewModel>(json);

            return View(book);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Tüm kitapları çek
                var response = await _httpClient.GetAsync("/api/Books");
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "API'den kitaplar alınamadı.";
                    return View(new List<BookViewModel>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<List<BookViewModel>>(json) ?? new List<BookViewModel>();

                // Favori bilgisi al
                var favResponse = await _httpClient.GetAsync("/api/Favorite/1"); // userId = 1
                if (favResponse.IsSuccessStatusCode)
                {
                    var favJson = await favResponse.Content.ReadAsStringAsync();

                    // Favorite nesnesi deserialize edilir
                    var favorite = JsonConvert.DeserializeObject<FavoriteViewModel>(favJson);
                    if (favorite != null)
                    {
                        foreach (var book in books)
                        {
                            if (book.Id == favorite.BookId)
                                book.IsFavorite = true;
                        }
                    }
                }

                return View(books);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Bir hata oluştu: " + ex.Message;
                return View(new List<BookViewModel>());
            }
        }
    }
}
