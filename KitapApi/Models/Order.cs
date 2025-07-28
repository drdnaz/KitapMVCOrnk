namespace KitapApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int Quantity { get; set; }
    }
}
