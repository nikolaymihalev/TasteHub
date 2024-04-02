using System.ComponentModel.DataAnnotations;

namespace TasteHub.Core.Models.User
{
    /// <summary>
    /// Model for login
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required]
        public string Email { get; set; } = null!;

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
