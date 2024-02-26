using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data.Configurations
{
    internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            var data = new SeedData();

            builder.HasKey(x => new { x.UserId, x.RecipeId });
            builder.HasOne(x => x.Recipe).WithMany(x => x.Ratings).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new Rating[] {data.FirstRating,data.SecondRating});
        }
    }
}
