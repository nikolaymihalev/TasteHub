using System.ComponentModel.DataAnnotations;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Core.Models.Admin
{
    public class QueryFormModel
    {        
        public string UserId { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.QueryDescriptionMaxLength,
            MinimumLength = ValidationConstants.QueryDescriptionMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;
    }
}
