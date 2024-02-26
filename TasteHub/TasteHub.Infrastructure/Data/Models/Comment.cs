using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Infrastructure.Data.Models
{
    [Comment("Comment entity")]
    public class Comment
    {
        [Comment("Comment identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Comment content")]
        [Required]
        [MaxLength(ValidationConstants.CommentContentMaxLength)]
        public string Content { get; set; } = string.Empty;

        [Comment("Date the comment was created")]
        [Required]
        public DateTime CreationDate { get; set; }

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
    }
}
