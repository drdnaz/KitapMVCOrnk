using Newtonsoft.Json;
using System.Net.Http;
using KitapMVCOrnk.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5079"); // ✅ KitapApi adresi
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("/api/Books");
        if (!response.IsSuccessStatusCode)
            return View(new List<BookViewModel>());

        var json = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<BookViewModel>>(json);

        return View(books ?? new List<BookViewModel>());
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult About()
    {
        return View();
    }

}