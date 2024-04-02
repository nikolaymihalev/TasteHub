using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteHub.Infrastructure.Data.Models
{
    [Comment("Rating entity")]
    public class Rating
    {
        [Comment("Rating identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Identifier of the user")]
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Comment("Identifier of the recipe")]
        [Required]
        public int RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        [Comment("Rating value")]
        [Required]
        public double Value { get; set; }
    }
}
