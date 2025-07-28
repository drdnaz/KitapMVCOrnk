namespace KitapMVCOrnk.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }= null!;
        public int CategoryId { get; set; }
        public bool IsFavorite { get; set; }
    }
}