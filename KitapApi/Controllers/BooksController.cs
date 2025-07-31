using Microsoft.AspNetCore.Mvc;
using KitapApi.Context;
using KitapApi.Models;
using KitapApi.Services;

namespace KitapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService bookService;

        public BooksController(BookService _bookService)
        {
            bookService = _bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books =await bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(Book book)
        {
            var createBook =await bookService.CreateBookAsync(book);
            return Ok(createBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
    }
}