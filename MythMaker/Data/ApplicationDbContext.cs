using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MythMaker.Models;

namespace MythMaker.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        private readonly string _wwwRootPath;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IWebHostEnvironment env)
        : base(options)
        {
            _wwwRootPath = env.WebRootPath; // Get the wwwroot directory
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Dragons", DisplayOrder = 1, ImagePath = "images/category-1.jpg"},
                new Category { Id = 2, Name = "Unicorns", DisplayOrder = 2, ImagePath = "images/category-2.jpg"}
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Luna, the Moonlit Watcher", DisplayOrder = 1, Price = 1.75, Description = "Meet Luna, the Moonlit Watcher. With scales that shift in shades of deep sapphire blue flecked with silver, Luna looks as if she’s bathed in starlight. Her emerald green eyes are fierce and intelligent, and her slender, curving horns add to her elegance. She watches over the world from rocky outcrops under a foggy, dawn-lit sky, surrounded by mystical wisps of smoke. Luna is calm, reflective, and perfect for those who feel drawn to moonlit mysteries and dreamlike landscapes.", ImagePath = "images/products/dragon-1", CategoryId = 1},
                new Product { Id = 2, Name = "Sylvandar, the Forest Guardian", DisplayOrder = 1, Price = 1.80, Description = "Meet Sylvandar, the Forest Guardian. With shimmering emerald green and teal scales flecked with gold, Sylvandar looks like a living piece of the forest itself. His amber eyes hold a wise yet powerful gaze, and his twisted, branch-like horns tell of ancient times. Sylvandar lives in a dense, misty forest, where sunlight filters softly through the trees at dusk, illuminating his presence. He’s a gentle giant who loves peace and balance, perfect for those who feel most at home among the trees and whispering leaves.", ImagePath = "images/products/dragon-2", CategoryId = 1 }
                );
        }

        //public override int SaveChanges()
        //{
        //    DeleteOrphanedProductImages();
        //    return base.SaveChanges();
        //}

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    DeleteOrphanedProductImages();
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        //private void DeleteOrphanedProductImages()
        //{
        //    // Detect products marked for deletion
        //    var deletedProducts = ChangeTracker.Entries<Product>()
        //        .Where(e => e.State == EntityState.Deleted)
        //        .Select(e => e.Entity)
        //        .ToList();

        //    foreach (var product in deletedProducts)
        //    {
        //        if (!string.IsNullOrEmpty(product.ImagePath))
        //        {
        //            // Combine the path to get the full file location
        //            var fullImagePath = Path.Combine(_wwwRootPath, product.ImagePath.TrimStart('\\'));

        //            if (File.Exists(fullImagePath))
        //            {
        //                File.Delete(fullImagePath); // Delete the image
        //            }
        //        }
        //    }
        //}
    }
}
