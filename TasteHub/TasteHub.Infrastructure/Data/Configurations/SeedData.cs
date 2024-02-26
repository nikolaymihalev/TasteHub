using Microsoft.AspNetCore.Identity;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data.Configurations
{
    internal class SeedData
    {
        public IdentityUser Creator { get; set; }
        public IdentityUser Guest { get; set; }
        public Category Sweets { get; set; }
        public Category Sandwiches { get; set; }
        public Recipe ChocolateCheesecake { get; set; }
        public Recipe Burger { get; set; }
        public Comment FirstComment { get; set; }
        public Comment SecondComment { get; set; }
        public Rating FirstRating { get; set; }
        public Rating SecondRating { get; set; }
        public FavoriteRecipe FirstFR { get; set; }

        private void SeedUsers() 
        {
            var hasher = new PasswordHasher<IdentityUser>();

            Creator = new IdentityUser()
            {
                Id = "6131b2c1-b80a-49ec-83ae-51fb006b5c89",
                UserName = "creator@mail.com",
                NormalizedUserName = "creator@mail.com",
                Email = "creator@mail.com",
                NormalizedEmail = "creator@mail.com"
            };

            Guest = new IdentityUser()
            {
                Id = "c208dab4-2a45-43e5-81dd-eb173111575b",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };

            Creator.PasswordHash = hasher.HashPassword(Creator, "creator1234");
            Guest.PasswordHash = hasher.HashPassword(Creator, "guest1234");
        }
    }
}
