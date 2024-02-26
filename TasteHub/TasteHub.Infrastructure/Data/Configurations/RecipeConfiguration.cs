using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data.Configurations
{
    internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            var data = new SeedData();

            builder.HasData(new Recipe[]{ data.ChocolateCheesecake, data.Burger });
        }
    }
}
