using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KitapMVCOrnk.Models; // Bunu ekle!

public class AdminController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5079"); // API'nın adresini doğru ayarla
    }

    public IActionResult Index()
    {
        // Sadece admin girebilsin:
        if (HttpContext.Session.GetString("UserRole") != "admin")
            return RedirectToAction("Login", "Home");
        return View();
    }

    public async Task<IActionResult> Categories()
    {
        if (HttpContext.Session.GetString("UserRole") != "admin")
            return RedirectToAction("Login", "Home");

        var response = await _httpClient.GetAsync("/api/Category");
        var json = await response.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(json);
        return View(categories);
    }
}
