﻿using KitapApi.Models;
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
                new Book { Id =1, Author="ali", CategoryId= 1, Price=12, Title="arabalar"});
            modelBuilder.Entity<User>().Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();
            
            modelBuilder.Entity<User>().HasData(
                new User { Id =1,UserName = "admin", Password = "admin123", Role = "admin" },
                new User { Id = 2, UserName = "user", Password = "user123", Role = "user" }
                );  
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserId = 1, OrderDate = DateTime.Now},
                new Order { Id = 2, UserId = 2, OrderDate = DateTime.Now.AddDays(-1) }
                );
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, BookId = 1, Quantity = 2 },
                new OrderItem { Id = 2, OrderId = 2, BookId = 1, Quantity = 1 }
            );
            modelBuilder.Entity<Favorite>().HasData(
                new Favorite { Id = 1, UserId = 1, BookId = 1 },
                new Favorite { Id = 2, UserId = 2, BookId = 1 }
            );
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
