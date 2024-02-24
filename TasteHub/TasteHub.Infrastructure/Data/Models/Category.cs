using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Data.Constants;

namespace TasteHub.Infrastructure.Data.Models
{
    [Comment("Category entity")]
    public class Category
    {
        [Comment("Category identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Category name")]
        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
