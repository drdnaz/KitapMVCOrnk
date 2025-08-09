using Newtonsoft.Json;
using System.Net.Http;
using KitapMVCOrnk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Session için
using System.Diagnostics;
using System.Text;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5079"); // KitapApi adresi
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

    public IActionResult About()
    {
        return View();
    }

    // GET: Bize Ulaşın sayfası
    public IActionResult Contact()
    {
        return View();
    }

    // POST: Bize Ulaşın formu gönderilince
    [HttpPost]
    public IActionResult Contact(string name, string email, string message)
    {
        ViewBag.Success = "Mesajınız başarıyla gönderildi!";
        return View();
    }

    // --- LOGIN ACTIONLARI EKLENDİ ---
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // API'dan kullanıcıyı çek (UserViewModel ile karşılaştır)
        var response = await _httpClient.GetAsync($"/api/User/login?username={username}&password={password}");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        var json = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserViewModel>(json);

        if (user == null)
        {
            ViewBag.Error = "Kullanıcı bulunamadı!";
            return View();
        }

        // Session'a rol ve id ekle
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserRole", user.Role);
        HttpContext.Session.SetString("UserName", user.UserName);

        return RedirectToAction("Index", "Home");
    }
    // --- /LOGIN ---

    // --- REGISTER ACTIONLARI EKLENDİ ---
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Tüm alanları doldurun!";
            return View();
        }

        // Kullanıcı API'ya POST ile eklenecek:
        var userObj = new { UserName = username, Password = password, Role = "user" };

        var response = await _httpClient.PostAsJsonAsync("/api/User", userObj);
        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
            return RedirectToAction("Login");
        }
        else
        {
            ViewBag.Error = "Kayıt sırasında hata oluştu. (Aynı isimde kullanıcı varsa bu hata olur)";
            return View();
        }
    }
    // --- /REGISTER ---

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
