using KitapApi.Context;
using KitapApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KitapApi.Services
{
    public class BookService
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var book = await _context.Books.AsNoTracking().ToListAsync();
            return book;
        }
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            var books = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == book.Id);
            if (books != null)
            {
                books.Title = book.Title;
                books.Author = book.Author;
                books.Price = book.Price;
                books.ImageUrl = book.ImageUrl;
                books.CategoryId = book.CategoryId;
                books.Id = book.Id;
                _context.Books.Update(books);
                await _context.SaveChangesAsync();
                return books;
            }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;
            _context.Books.Remove(book);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? true : false;
        }

    }
}
