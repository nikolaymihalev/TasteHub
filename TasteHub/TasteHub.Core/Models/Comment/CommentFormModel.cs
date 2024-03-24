using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models.Comment
{
    /// <summary>
    /// Comment model for adding or editing
    /// </summary>
    public class CommentFormModel
    {
        /// <summary>
        /// Comment content
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.CommentContentMaxLength,
            MinimumLength = ValidationConstants.CommentContentMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Recipe identifier
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public int RecipeId { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}
