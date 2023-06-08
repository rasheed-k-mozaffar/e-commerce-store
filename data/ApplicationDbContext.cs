using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }  
        public DbSet<Cart> Carts { get; set; }      
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderItem> OrderItems { get; set; }    
        public DbSet<Product> Products { get; set; }    
        

        
    }
}