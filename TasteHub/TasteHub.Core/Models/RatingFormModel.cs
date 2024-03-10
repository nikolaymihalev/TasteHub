using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models
{
    public class RatingFormModel
    {
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [Range(ValidationConstants.RatingMinValue,
            ValidationConstants.RatingMaxValue,
            ErrorMessage = ErrorMessageConstants.ValueErrorMessage)]
        public double Value { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public int RecipeId { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
