using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models
{
    public class CommentFormModel
    {
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.CommentContentMaxLength,
            MinimumLength = ValidationConstants.CommentContentMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public int RecipeId { get; set; }
    }
}
