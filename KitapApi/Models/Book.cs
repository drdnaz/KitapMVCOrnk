

namespace KitapApi.Models

{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
     
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
