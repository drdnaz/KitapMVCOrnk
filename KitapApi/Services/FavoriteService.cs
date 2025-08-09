using KitapApi.Context;
using KitapApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KitapApi.Services
{
    public class FavoriteService
    {
        private readonly AppDbContext _context;

        public FavoriteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetAllFavoritesAsync()
        {
            return await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Book)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Favorite?> GetFavoriteByIdAsync(int id)
        {
            return await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Favorite> CreateOrUpdateFavoriteAsync(Favorite favorite)
        {
            // KULLANICI ve KİTAP BAZINDA ARAMA YAP!
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.BookId == favorite.BookId);

            if (existingFavorite != null) { 
                return existingFavorite; // Zaten eklenmişse tekrar ekleme

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }


            else
            {
                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
                return favorite;
            }
        }

        public async Task<bool> DeleteFavoriteAsync(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
