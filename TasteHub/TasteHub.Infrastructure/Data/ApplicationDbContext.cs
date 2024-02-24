using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Rating>().HasKey(x => new { x.UserId, x.RecipeId });
            builder.Entity<FavoriteRecipe>().HasKey(x => new { x.UserId, x.RecipeId });

            builder.Entity<Rating>().HasOne(x => x.Recipe).WithMany(x => x.Ratings).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<FavoriteRecipe>().HasOne(x => x.Recipe).WithMany(x => x.FavoriteRecipes).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
