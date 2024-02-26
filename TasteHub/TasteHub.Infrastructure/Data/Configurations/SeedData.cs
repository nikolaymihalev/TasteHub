using Microsoft.AspNetCore.Identity;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Infrastructure.Data.Configurations
{
    internal class SeedData
    {
        public SeedData()
        {
            SeedUsers();
            SeedCategories();
            SeedRecipes();
            SeedComments();
            SeedRatings();
            SeedFavoriteRecipes();
        }
        public IdentityUser Creator { get; private set; }
        public IdentityUser Guest { get; private set; }
        public Category Sweets { get; private set; }
        public Category Sandwiches { get; private set; }
        public Recipe ChocolateCheesecake { get; private set; }
        public Recipe Burger { get; private set; }
        public Comment FirstComment { get; private set; }
        public Comment SecondComment { get; private set; }
        public Rating FirstRating { get; private set; }
        public Rating SecondRating { get; private set; }
        public FavoriteRecipe FirstFR { get; private set; }

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

        private void SeedCategories()
        {
            Sweets = new Category()
            {
                Id = 1,
                Name = "Sweets"
            };

            Sandwiches = new Category()
            {
                Id = 2,
                Name = "Sandwiches"
            };
        }

        private void SeedRecipes() 
        {
            ChocolateCheesecake = new Recipe()
            {
                Id = 1,
                Title = "Chocolate cheesecake",
                Description = "Have a sweet tooth with my special occasion chocolate cheesecake!",
                Ingredients = "biscuits - 200 g, cocoa\r\ncow butter - 60 g\r\ncream cheese - 500 g\r\nchocolate - 160 g (50% cocoa)\r\nsugar - 100 g, brown\r\neggs - 2 pcs.\r\nwhiskey - 20 ml\r\nFOR THE CHOCOLATE GANACHE\r\nchocolate - 160 g (50% cocoa)\r\ncream - 160 g confectionery (30% fat)\r\ncow butter - 50 g\r\nFOR DECORATION\r\nfresh fruit - optional\r\nsugar pearls",
                Instructions = "The bottom of the pan, in which we will prepare the chocolate cheesecake, is lined with baking paper. On the outside, it is covered with two layers of aluminum foil, and the goal is to prevent water from entering the mold during baking, as it will be in a water bath.\r\nIt is important to note that if you are going to make walls of the chocolate cheesecake from the biscuit mixture, it is good that the walls of the pan can be removed.\r\nIf you are not going to make walls, but only a base, then the amount of biscuits should be reduced to 120 g. Cocoa biscuits are crushed in a chopper and then mixed with melted cow butter, mixed until homogeneous. The biscuit mixture is poured into the prepared baking form. If walls are also being made for the cheesecake, shape the base with the back of a spoon and start with the walls first and finish with the bottom. The form is stored in the refrigerator.\r\nFor the cheesecake mixture, first, melt the chocolate in a water bath and set it aside. Mix the cream cheese, which should be at room temperature, with the brown sugar and beat with a mixer until the sugar dissolves and the mixture becomes creamy.\r\nAdd the cocoa powder and gradually add the melted chocolate. Finally, the eggs and whiskey are added to the mixture. Stir very lightly with a spatula or wire whisk just to combine. The finished mixture is poured into the baking dish on the biscuit base with butter.\r\nThe baking form is placed in a larger and deeper tray, into which warm water is poured. The tray is placed in the middle of the oven, which is preheated to 160 °C. Bake for 60 minutes, then leave the cheesecake in the oven with the door slightly ajar until it cools completely. That way it won't drop sharply and crack. Once completely cooled, the cheesecake is wrapped in plastic wrap without removing from the baking pan and placed in the refrigerator overnight to set.\r\nFor the chocolate ganache, melt 160 g of chocolate (50% cocoa or more) in a water bath and gradually add the cream, which must be at room temperature. Stir carefully with a silicone spatula until the chocolate is completely dissolved in the cream. Finally, add cow butter and stir until it is also absorbed by the mixture. Mix with a blender until the mixture becomes homogeneous and shiny. Set aside to cool slightly.\r\nWith a sharp knife, carefully run the sides of the baking dish to separate the cheesecake more easily. The ring opens and the walls are removed.\r\nUsing the paper on the bottom and a spatula, the cheesecake is separated from the bottom and transferred to a suitable serving plate. You can use a knife to slightly level the protruding edges of the walls.\r\nPour over the prepared, slightly cooled chocolate ganache, reserving a small amount for decoration. Cover with the lid of the cake plate and place in the refrigerator for an hour.\r\nThe separated amount of chocolate ganache is transferred to a posh and stored in the refrigerator to harden a little. It is later used to decorate the cheesecake, as you can inject roses or form other decorations to your taste. Finally, the finished chocolate cheesecake is decorated with sugar pearls and fresh fruit of your choice.\r\nIt is cut into portioned pieces with a dry heated knife, and after each cut the knife must be dried again.",
                Image = File.ReadAllBytes(Path.Combine(@"..\..\Images","Cheesecake.png")),
                CreationDate = DateTime.Now,
                CreatorId = Creator.Id,
                CategoryId = Sweets.Id
            };

            Burger = new Recipe()
            {
                Id = 2,
                Title = "Burger",
                Ingredients = "beef - 900 g (chopped)\r\npickles - 1 pc.\r\ngreen onion - 2 - 3 feathers\r\nsalt\r\npepper\r\nketchup",
                Instructions = "Cucumbers and onions are finely chopped. Add the meat, black pepper and salt.\r\nMix well and divide into 6 balls. The balls are pressed until they are flat, placed in a pan and baked in a moderate oven. They are garnished with ketchup.",
                Image = File.ReadAllBytes(Path.Combine(@"..\..\Images", "Burger.png")),
                CreationDate = DateTime.Now,
                CreatorId = Guest.Id,
                CategoryId = Sandwiches.Id
            };
        }

        private void SeedComments()
        {
            FirstComment = new Comment()
            {
                Id = 1,
                Content = "Amazing recipe!",
                CreationDate = DateTime.Now,
                UserId = Guest.Id,
                RecipeId = ChocolateCheesecake.Id,
            };

            SecondComment = new Comment()
            {
                Id = 2,
                Content = "Well done!",
                CreationDate = DateTime.Now,
                UserId = Creator.Id,
                RecipeId = Burger.Id,
            };
        }

        private void SeedRatings()
        {
            FirstRating = new Rating()
            {
                UserId = Guest.Id,
                RecipeId = ChocolateCheesecake.Id,
                Value = 5
            };

            SecondRating = new Rating()
            {
                UserId = Creator.Id,
                RecipeId = Burger.Id,
                Value = 4.6
            };
        }

        private void SeedFavoriteRecipes()
        {
            FirstFR = new FavoriteRecipe()
            {
                UserId = Creator.Id,
                RecipeId = Burger.Id
            };
        }
    }
}