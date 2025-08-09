using KitapApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
namespace KitapApi.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>().HasData(
    new Category { Id = 1, Name = "Fiction" },
    new Category { Id = 2, Name = "Non-Fiction" },
    new Category { Id = 3, Name = "Science" },
    new Category { Id = 4, Name = "History" }
);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Author = "Victor Higo", CategoryId = 1, Price = 49, Title = "sefiller" },
                new Book { Id = 2, Author = "stefan zweig", CategoryId = 2, Price = 55, Title = "satranç" },
                new Book { Id = 3, Author = "deneme", CategoryId = 1, Price = 77, Title = "TestKitap" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id =1,UserName = "admin", Password = "admin123", Role = "admin" },
                new User { Id = 2, UserName = "user", Password = "user123", Role = "user" }
                );  
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserId = 1, OrderDate = new DateTime(2025, 1, 1) },
                new Order { Id = 2, UserId = 2, OrderDate = new DateTime(2025, 1, 1) }
                );
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, BookId = 1, Quantity = 2 },
                new OrderItem { Id = 2, OrderId = 2, BookId = 1, Quantity = 1 }
            );
            modelBuilder.Entity<Favorite>().HasData(
                new Favorite { Id = 1, UserId = 1, BookId = 1 },
                new Favorite { Id = 2, UserId = 2, BookId = 1 }
            );
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);
            modelBuilder.Entity<Favorite>()
                .HasOne(c=>c.Book)
                .WithMany(b => b.Favorites)
                .HasForeignKey(f => f.BookId);
        }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }


    }

}
