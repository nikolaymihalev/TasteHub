using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models.Rating
{
    public class RatingFormModel
    {
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [Range(ValidationConstants.RatingMinValue,
            ValidationConstants.RatingMaxValue,
            ErrorMessage = ErrorMessageConstants.ValueErrorMessage)]
        public int Value { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public int RecipeId { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
