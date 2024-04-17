namespace TasteHub.UnitTests
{
    [TestFixture]
    public class FavoriteRecipeServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private FavoriteRecipe favoriteRecipe;
        private IdentityUser user;
        private IdentityUser guest;
        private IdentityUser tester;
        private Recipe recipe;
        private Category category;
        private ILogger<FavoriteRecipeService> logger;
        private IFavoriteRecipeService favoriteRecipeService;

        [SetUp]
        public void SetUp()
        {
            user = new IdentityUser()
            {
                Id = "261d1ded-cecc-4f10-80e9-b192247bb14f",
                UserName = "user@mail.com",
                NormalizedUserName = "user@mail.com",
                Email = "user@mail.com",
                NormalizedEmail = "user@mail.com"
            };
            
            guest = new IdentityUser()
            {
                Id = "728146da-7fce-4d15-882e-73b69fd17832",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };
            
            tester = new IdentityUser()
            {
                Id = "236fbb4a-b60c-48c6-854a-828819d9d03a",
                UserName = "tester@mail.com",
                NormalizedUserName = "tester@mail.com",
                Email = "tester@mail.com",
                NormalizedEmail = "tester@mail.com"
            };

            recipe = new Recipe()
            {
                Title = "Chocolate cheesecake",
                Description = "Have a sweet tooth with my special occasion chocolate cheesecake!",
                Ingredients = "biscuits - 200 g, cocoa; cow butter - 60 g; cream cheese - 500 g; chocolate - 160 g (50% cocoa); sugar - 100 g, brown; eggs - 2 pcs.; whiskey - 20 ml; FOR THE CHOCOLATE GANACHE; chocolate - 160 g (50% cocoa); cream - 160 g confectionery (30% fat); cow butter - 50 g; FOR DECORATION; fresh fruit - optional; sugar pearls",
                Instructions = "The bottom of the pan, in which we will prepare the chocolate cheesecake, is lined with baking paper. On the outside, it is covered with two layers of aluminum foil, and the goal is to prevent water from entering the mold during baking, as it will be in a water bath. It is important to note that if you are going to make walls of the chocolate cheesecake from the biscuit mixture, it is good that the walls of the pan can be removed. If you are not going to make walls, but only a base, then the amount of biscuits should be reduced to 120 g. Cocoa biscuits are crushed in a chopper and then mixed with melted cow butter, mixed until homogeneous. The biscuit mixture is poured into the prepared baking form. If walls are also being made for the cheesecake, shape the base with the back of a spoon and start with the walls first and finish with the bottom. The form is stored in the refrigerator. For the cheesecake mixture, first, melt the chocolate in a water bath and set it aside. Mix the cream cheese, which should be at room temperature, with the brown sugar and beat with a mixer until the sugar dissolves and the mixture becomes creamy.\r\nAdd the cocoa powder and gradually add the melted chocolate. Finally, the eggs and whiskey are added to the mixture. Stir very lightly with a spatula or wire whisk just to combine. The finished mixture is poured into the baking dish on the biscuit base with butter. The baking form is placed in a larger and deeper tray, into which warm water is poured. The tray is placed in the middle of the oven, which is preheated to 160 °C. Bake for 60 minutes, then leave the cheesecake in the oven with the door slightly ajar until it cools completely. That way it won't drop sharply and crack. Once completely cooled, the cheesecake is wrapped in plastic wrap without removing from the baking pan and placed in the refrigerator overnight to set. For the chocolate ganache, melt 160 g of chocolate (50% cocoa or more) in a water bath and gradually add the cream, which must be at room temperature. Stir carefully with a silicone spatula until the chocolate is completely dissolved in the cream. Finally, add cow butter and stir until it is also absorbed by the mixture. Mix with a blender until the mixture becomes homogeneous and shiny. Set aside to cool slightly. With a sharp knife, carefully run the sides of the baking dish to separate the cheesecake more easily. The ring opens and the walls are removed.\r\nUsing the paper on the bottom and a spatula, the cheesecake is separated from the bottom and transferred to a suitable serving plate. You can use a knife to slightly level the protruding edges of the walls. Pour over the prepared, slightly cooled chocolate ganache, reserving a small amount for decoration. Cover with the lid of the cake plate and place in the refrigerator for an hour. The separated amount of chocolate ganache is transferred to a posh and stored in the refrigerator to harden a little. It is later used to decorate the cheesecake, as you can inject roses or form other decorations to your taste. Finally, the finished chocolate cheesecake is decorated with sugar pearls and fresh fruit of your choice. It is cut into portioned pieces with a dry heated knife, and after each cut the knife must be dried again.",
                Image = File.ReadAllBytes(Path.Combine(@"Images", "Cheesecake.png")),
                CreationDate = DateTime.Now,
                CreatorId = user.Id,
                CategoryId = 1,
            };

            category = new Category()
            {
                Name = "Pizza"
            };

            favoriteRecipe = new FavoriteRecipe()
            {
                RecipeId = 1,
                UserId = guest.Id,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync<FavoriteRecipe>(this.favoriteRecipe);
            this.repository.AddAsync<Recipe>(recipe);
            this.repository.AddAsync<Category>(category);
            this.repository.AddAsync<IdentityUser>(user);
            this.repository.AddAsync<IdentityUser>(guest);
            this.repository.AddAsync<IdentityUser>(tester);
            this.repository.SaveChangesAsync();

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<FavoriteRecipeService>();

            favoriteRecipeService = new FavoriteRecipeService(this.repository, logger);
        }

        [Test]
        public void Test_Add()
        {
            int expectedCount = 2;

            var model = new FavoriteRecipeInfoModel(
                user.Id,
                1,
                null);

            _ = favoriteRecipeService.AddAsync(model);

            int actualCount = favoriteRecipeService.GetAllFavoriteRecipesAsync().Result.Count();

            Assert.IsTrue(expectedCount == actualCount);
        }

        [Test]
        public void Test_AddShouldThrowException() 
        {
            string expectedException = "Operation failed. Try again!";

            var model = new FavoriteRecipeInfoModel(
                guest.Id,
                1,
                null);

            var result = favoriteRecipeService.AddAsync(model).Exception.InnerException.Message;

            Assert.IsTrue(expectedException == result);
        }

        [Test]
        public void Test_GetAllFavoriteRecipes()
        {
            int expectedCount = 2;

            int actualCount = favoriteRecipeService.GetAllFavoriteRecipesAsync().Result.Count();

            Assert.IsTrue(expectedCount == actualCount);
        }

        [Test]
        public void Test_GetUserFavoriteRecipes()
        {
            int expectedCount = 1;
            int expectedRecipeId = 1;
            string expectedUserId = "728146da-7fce-4d15-882e-73b69fd17832";

            var favoriteRecipes = favoriteRecipeService.GetAllFavoriteRecipesForUserAsync(guest.Id).Result.ToList();
            var firstFavoriteRecipe = favoriteRecipes[0];

            Assert.IsTrue(expectedCount == favoriteRecipes.Count());
            Assert.IsTrue(firstFavoriteRecipe != null);
            Assert.IsTrue(expectedRecipeId == firstFavoriteRecipe.RecipeId);
            Assert.IsTrue(expectedUserId == firstFavoriteRecipe.UserId);
        }

        [Test]
        public void Test_DeleteShouldThrowException()
        {
            var expectedException = "Invalid favorite recipe!";

            var result = favoriteRecipeService.DeleteAsync(1000,"invalidid").Exception.InnerException.Message;

            Assert.IsTrue(expectedException == result);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
