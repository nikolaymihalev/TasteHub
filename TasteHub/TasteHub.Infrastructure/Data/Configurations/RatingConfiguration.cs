using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RecipeId });
            builder.HasOne(x => x.Recipe).WithMany(x => x.Ratings).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
