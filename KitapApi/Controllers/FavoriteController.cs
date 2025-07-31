using Microsoft.AspNetCore.Mvc;
using KitapApi.Models;
using KitapApi.Services;

namespace KitapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly FavoriteService _favoriteService;

        public FavoriteController(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var favorites = await _favoriteService.GetAllFavoritesAsync();
            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var favorite = await _favoriteService.GetFavoriteByIdAsync(id);
            if (favorite == null) return NotFound();
            return Ok(favorite);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(Favorite favorite)
        {
            var result = await _favoriteService.CreateOrUpdateFavoriteAsync(favorite);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _favoriteService.DeleteFavoriteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
