namespace KitapApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "user";
        public ICollection<Favorite>? Favorites { get; set; }

    }
}
