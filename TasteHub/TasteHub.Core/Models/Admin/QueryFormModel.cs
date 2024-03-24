using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models.Admin
{
    /// <summary>
    /// Query model for adding or editing
    /// </summary>
    public class QueryFormModel
    {        
        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Query description
        /// </summary>
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.QueryDescriptionMaxLength,
            MinimumLength = ValidationConstants.QueryDescriptionMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;
    }
}
