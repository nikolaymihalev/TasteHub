using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasteHub.Infrastructure.Constants;

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

        [Comment("Image of the food")]
        [Required]
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Image { get; set; }

        [Comment("Recipe creator identifier")]
        [Required]
        public string CreatorId { get; set; } = string.Empty;

        [ForeignKey(nameof(CreatorId))]
        public IdentityUser Creator { get; set; } = null!;

        [Comment("Recipe category identifier")]
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public IList<Comment> Comments { get; set; } = new List<Comment>();
        public IList<Rating> Ratings { get; set; } = new List<Rating>();
        public IList<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();
    }
}
