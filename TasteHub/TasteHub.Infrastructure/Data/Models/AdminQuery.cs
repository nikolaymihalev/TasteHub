using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Infrastructure.Data.Models
{
    [Comment("Query for becoming admin")]
    public class AdminQuery
    {
        [Comment("Query identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("User identifier")]
        [Required]
        public string UserId { get; set; } = null!;

        [Comment("Query description")]
        [Required]
        [MaxLength(ValidationConstants.QueryDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
