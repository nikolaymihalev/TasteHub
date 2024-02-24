using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasteHub.Infrastructure.Data.Constants;

namespace TasteHub.Infrastructure.Data.Models
{
    [Comment("Recipe entity")]
    public class Recipe
    {
        [Comment("Recipe identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Recipe title")]
        [Required]
        [MaxLength(ValidationConstants.RecipeTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Comment("Recipe description")]
        public string Description { get; set; } = string.Empty;

        [Comment("Recipe ingredients")]
        [Required]
        [MaxLength(ValidationConstants.RecipeIngredientsMaxLength)]
        public string Ingredients { get; set; } = string.Empty;

        [Comment("Recipe instructions")]
        [Required]
        [MaxLength(ValidationConstants.RecipeInstructionsMaxLength)]
        public string Instructions { get; set; } = string.Empty;

        [Comment("Date the recipe was created")]
        [Required]
        public DateTime CreationDate { get; set; }

        [Comment("Recipe creator")]
        [Required]
        public string CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public IdentityUser Creator { get; set; }

        [Comment("Recipe category identifier")]
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
