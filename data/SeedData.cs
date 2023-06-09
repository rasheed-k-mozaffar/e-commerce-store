using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }
                context.Products.AddRange(
                new Product
                {
                    Name = "Mobile",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    CategoryId = 1,
                    Price = 4000,
                    SKU = "8hf877dg8u4i4" ,
                    Derscription = "its iphone",
                    ImageURL = "wwwroot/Image/phone1.png"

                },
                new Product
                {
                    Name = "Mobile2",
                    ReleaseDate = DateTime.Parse("2000-2-12"),
                    CategoryId = 1,
                    Price = 4000,
                    SKU = "8hf8737dg8u4i4" ,
                    Derscription = "its samsung",
                    ImageURL = "wwwroot/Image/phone2.png"
                }
                );
                context.SaveChanges();
            }
        }
    }//SeedData
}//namespace