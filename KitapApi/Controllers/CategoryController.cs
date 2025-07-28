using Microsoft.AspNetCore.Mvc;
using KitapApi.Context;
using KitapApi.Models;

namespace KitapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Category updatedCategory)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            category.Name = updatedCategory.Name;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
    }
}