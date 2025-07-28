using Microsoft.AspNetCore.Mvc;
using KitapApi.Context;
using KitapApi.Models;

namespace KitapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book updatedBook)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Price = updatedBook.Price;
            book.ImageUrl = updatedBook.ImageUrl;
            book.CategoryId = updatedBook.CategoryId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }
    }
}