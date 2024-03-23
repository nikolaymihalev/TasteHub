using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for Comment entity
    /// </summary>
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            var data = new SeedData();

            builder.HasOne(x => x.Recipe).WithMany(x => x.Comments).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new Comment[]{ data.FirstComment, data.SecondComment });
        }
    }
}
