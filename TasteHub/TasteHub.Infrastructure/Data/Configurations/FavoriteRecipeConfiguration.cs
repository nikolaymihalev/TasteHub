using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data.Configurations
{
    public class FavoriteRecipeConfiguration : IEntityTypeConfiguration<FavoriteRecipe>
    {
        public void Configure(EntityTypeBuilder<FavoriteRecipe> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RecipeId });
            builder.HasOne(x => x.Recipe).WithMany(x => x.FavoriteRecipes).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
