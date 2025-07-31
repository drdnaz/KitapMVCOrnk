using KitapApi.Context;
using KitapApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KitapApi.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var category = await _context.Categories.AsNoTracking().ToListAsync();
            return category;
        }
     
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
       
        public async Task<Category> CreateCategoryAsync(Category category)
        {

            var categories = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(b => b.Id == category.Id);
            if (categories != null)
            { 
                categories.Name = category.Name;
                categories.Id = category.Id;
                _context.Update(categories);
                await _context.SaveChangesAsync();
                return categories;
            }
            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;
            _context.Categories.Remove(category);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? true : false;
        }


    }
}
