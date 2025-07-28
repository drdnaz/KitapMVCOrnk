using KitapApi.Context;
using KitapApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KitapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoriteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddFavorite(Favorite favorite)
        {
            // Aynı favori var mı kontrolü (opsiyonel)
            var existing = _context.Favorites
                .FirstOrDefault(f => f.UserId == favorite.UserId && f.BookId == favorite.BookId);

            if (existing != null)
            {
                return Conflict("Bu kitap zaten favorilerde.");
            }

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            return Ok(favorite);
        }
      

        [HttpGet("{userId}")]
        public IActionResult GetFavoritesByUser(int userId)
        {
            var favorites = _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.Book)
                .ToList();

            return Ok(favorites);
        }
 

        [HttpDelete("{bookId}")]
        public IActionResult RemoveFavorite(int bookId)
        {
            // Şu anlık kullanıcı sabit: 1 (ileride login olunca alınabilir)
            int userId = 1;

            var favorite = _context.Favorites
                .FirstOrDefault(f => f.UserId == userId && f.BookId == bookId);

            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            _context.SaveChanges();

            return NoContent(); 
        }
    }
}