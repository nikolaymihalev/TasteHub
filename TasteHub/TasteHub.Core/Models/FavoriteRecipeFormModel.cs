using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models
{
    /// <summary>
    /// Model for adding or edditing favorite recipe
    /// </summary>
    public class FavoriteRecipeFormModel
    {
        /// <summary>
        /// Creator identifier
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public string CreatorId { get; set; } = string.Empty;

        /// <summary>
        /// Recipe identifier
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public int RecipeId { get; set; }
    }
}
