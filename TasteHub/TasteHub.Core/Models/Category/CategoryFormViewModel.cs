using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models.Category
{
    /// <summary>
    /// Model for adding or edditing category
    /// </summary>
    public class CategoryFormViewModel
    {
        /// <summary>
        /// Category identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.CategoryNameMaxLength,
            MinimumLength = ValidationConstants.CategoryNameMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
