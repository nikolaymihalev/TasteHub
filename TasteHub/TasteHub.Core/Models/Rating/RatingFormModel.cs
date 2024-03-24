using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models.Rating
{
    /// <summary>
    /// Rating model for adding or editing
    /// </summary>
    public class RatingFormModel
    {
        /// <summary>
        /// Rating value
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [Range(ValidationConstants.RatingMinValue,
            ValidationConstants.RatingMaxValue,
            ErrorMessage = ErrorMessageConstants.ValueErrorMessage)]
        public int Value { get; set; }

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
