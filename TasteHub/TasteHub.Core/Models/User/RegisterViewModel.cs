using System.ComponentModel.DataAnnotations;

namespace TasteHub.Core.Models.User
{
    /// <summary>
    /// Model for register
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// User username
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// User email
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// User confirm password
        /// </summary>
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
