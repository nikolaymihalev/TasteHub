using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models
{
    /// <summary>
    /// Model for adding or edditing recipe
    /// </summary>
    public class RecipeFormViewModel
    {
        /// <summary>
        /// Recipe identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Recipe title
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.RecipeTitleMaxLength, 
            MinimumLength = ValidationConstants.RecipeTitleMinLength, 
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Recipe description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Recipe ingredients
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.RecipeIngredientsMaxLength,
            MinimumLength = ValidationConstants.RecipeIngredientsMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Ingredients { get; set; } = string.Empty;

        /// <summary>
        /// Instructions for making the recipe
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.RecipeInstructionsMaxLength,
             MinimumLength = ValidationConstants.RecipeInstructionsMinLength,
             ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Instructions { get; set; } = string.Empty;

        /// <summary>
        /// Date of creation
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        //Binding format needed
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Image of the food
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public byte[] Image { get; set; }

        /// <summary>
        /// Category identifier
        /// </summary>
        public int CategoryId { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public string CreatorId { get; set; } = string.Empty;

        /// <summary>
        /// Collection of categories
        /// </summary>
        public IEnumerable<CategoryInfoViewModel> Categories { get; set; } = new List<CategoryInfoViewModel>();
    }
}