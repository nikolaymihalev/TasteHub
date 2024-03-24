using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models.Admin
{
    /// <summary>
    /// Role model for adding or editing
    /// </summary>
    public class RoleFormModel
    {
        /// <summary>
        /// Role name
        /// </summary>
        [Display(Name = "Role")]
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.RoleNameMaxLength,
            MinimumLength = ValidationConstants.RoleNameMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
