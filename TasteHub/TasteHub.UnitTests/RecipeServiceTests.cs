using TasteHub.Core.Models.Recipe;

namespace TasteHub.UnitTests
{
    [TestFixture]
    public class RecipeServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IdentityUser user;
        private Recipe recipe;
        private Category category;
        private ILogger<RecipeService> logger;
        private IRecipeService recipeService;

        [SetUp]
        public void SetUp()
        {
            user = new IdentityUser()
            {
                Id = "9e59b694-139f-4eb8-91ba-b54ba7fa4b10",
                UserName = "user@mail.com",
                NormalizedUserName = "user@mail.com",
                Email = "user@mail.com",
                NormalizedEmail = "user@mail.com"
            };

            recipe = new Recipe()
            {
                Title = "Cheesecake",
                Description = "Have a sweet tooth!",
                Ingredients = "biscuits - 200 g",
                Instructions = "The bottom of the pan",
                Image = File.ReadAllBytes(Path.Combine(@"Images", "Cheesecake.png")),
                CreationDate = DateTime.Parse("25/01/2021"),
                CreatorId = user.Id,
                CategoryId = 1,
            };

            category = new Category()
            {
                Name = "Noodles"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync<Recipe>(recipe);
            this.repository.AddAsync<Category>(category);
            this.repository.AddAsync<IdentityUser>(user);
            this.repository.SaveChangesAsync();

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<RecipeService>();

            recipeService = new RecipeService(this.repository, logger);
        }

        [Test]
        public void Test_AddShouldThrowException()
        {
            string expectedException = "Operation failed. Try again!";

            var model = new RecipeFormViewModel()
            {
                Title = "Cheesecake",
                Description = "Have a sweet tooth!",
                Ingredients = "biscuits - 200 g",
                Instructions = "The bottom of the pan",
                Image = File.ReadAllBytes(Path.Combine(@"Images", "Cheesecake.png")),
                CreationDate = DateTime.Parse("25/01/2021"),
                CreatorId = user.Id,
                CategoryId = 1,
            };

            string actualException = recipeService.AddAsync(model).Exception.InnerException.Message;

            Assert.IsTrue(expectedException == actualException);
        }

        [Test]
        public void Test_Add()
        {
            //5 because of other test classes otherwise change to 2
            int expectedCount = 5;

            var model = new RecipeFormViewModel()
            {
                Title = "Cookies",
                Ingredients = "biscuits - 200 g, cocoa;",
                Instructions = "The bottom of the pan, in which we will prepare the chocolate cheesecake, is lined with baking paper. On the outside, it is covered with two layers of aluminum foil, and the goal is to prevent water from entering the mold during baking, as it will be in a water bath. It is important to note that if you are going to make walls of the chocolate cheesecake from the biscuit mixture, it is good that the walls of the pan can be removed. If you are not going to make walls, but only a base, then the amount of biscuits should be reduced to 120 g. Cocoa biscuits are crushed in a chopper and then mixed with melted cow butter, mixed until homogeneous. The biscuit mixture is poured into the prepared baking form. If walls are also being made for the cheesecake, shape the base with the back of a spoon and start with the walls first and finish with the bottom. The form is stored in the refrigerator. For the cheesecake mixture, first, melt the chocolate in a water bath and set it aside. Mix the cream cheese, which should be at room temperature, with the brown sugar and beat with a mixer until the sugar dissolves and the mixture becomes creamy.\r\nAdd the cocoa powder and gradually add the melted chocolate. Finally, the eggs and whiskey are added to the mixture. Stir very lightly with a spatula or wire whisk just to combine. The finished mixture is poured into the baking dish on the biscuit base with butter. The baking form is placed in a larger and deeper tray, into which warm water is poured. The tray is placed in the middle of the oven, which is preheated to 160 °C. Bake for 60 minutes, then leave the cheesecake in the oven with the door slightly ajar until it cools completely. That way it won't drop sharply and crack. Once completely cooled, the cheesecake is wrapped in plastic wrap without removing from the baking pan and placed in the refrigerator overnight to set. For the chocolate ganache, melt 160 g of chocolate (50% cocoa or more) in a water bath and gradually add the cream, which must be at room temperature. Stir carefully with a silicone spatula until the chocolate is completely dissolved in the cream. Finally, add cow butter and stir until it is also absorbed by the mixture. Mix with a blender until the mixture becomes homogeneous and shiny. Set aside to cool slightly. With a sharp knife, carefully run the sides of the baking dish to separate the cheesecake more easily. The ring opens and the walls are removed.\r\nUsing the paper on the bottom and a spatula, the cheesecake is separated from the bottom and transferred to a suitable serving plate. You can use a knife to slightly level the protruding edges of the walls. Pour over the prepared, slightly cooled chocolate ganache, reserving a small amount for decoration. Cover with the lid of the cake plate and place in the refrigerator for an hour. The separated amount of chocolate ganache is transferred to a posh and stored in the refrigerator to harden a little. It is later used to decorate the cheesecake, as you can inject roses or form other decorations to your taste. Finally, the finished chocolate cheesecake is decorated with sugar pearls and fresh fruit of your choice. It is cut into portioned pieces with a dry heated knife, and after each cut the knife must be dried again.",
                Image = File.ReadAllBytes(Path.Combine(@"Images", "Cheesecake.png")),
                CreationDate = DateTime.Parse("26/02/2024"),
                CreatorId = user.Id,
                CategoryId = 1,
            };

            _ = recipeService.AddAsync(model);

            int actualCount = recipeService.GetAllRecipesAsync().Result.Count();

            Assert.IsTrue(expectedCount == actualCount);
        }

        [Test]
        public void Test_DeleteShouldThrowException()
        {
            string expectedException = "Invalid recipe!";

            string actualException = recipeService.DeleteAsync(10000).Exception.InnerException.Message;

            Assert.IsTrue(expectedException == actualException);
        }

        [Test]
        public void Test_Delete() 
        {
            //7 because of other test classes otherwise change to 1
            int expectedCount = 7;

            //21 because of other test classes otherwise change to 2
            _ = recipeService.DeleteAsync(21);

            int actualCount = recipeService.GetAllRecipesAsync().Result.Count();

            Assert.IsTrue(expectedCount == actualCount);
        }

        [Test]
        public void Test_EditShouldThrowException() 
        {
            string expectedException = "Invalid recipe!";

            var model = new RecipeFormViewModel()
            {
                Id = 20000,
                Title = "Cookies",
                Ingredients = "biscuits - 200 g, cocoa;",
                Instructions = "The bottom of the pan, in which we will prepare the chocolate cheesecake, is lined with baking paper. On the outside, it is covered with two layers of aluminum foil, and the goal is to prevent water from entering the mold during baking, as it will be in a water bath. It is important to note that if you are going to make walls of the chocolate cheesecake from the biscuit mixture, it is good that the walls of the pan can be removed. If you are not going to make walls, but only a base, then the amount of biscuits should be reduced to 120 g. Cocoa biscuits are crushed in a chopper and then mixed with melted cow butter, mixed until homogeneous. The biscuit mixture is poured into the prepared baking form. If walls are also being made for the cheesecake, shape the base with the back of a spoon and start with the walls first and finish with the bottom. The form is stored in the refrigerator. For the cheesecake mixture, first, melt the chocolate in a water bath and set it aside. Mix the cream cheese, which should be at room temperature, with the brown sugar and beat with a mixer until the sugar dissolves and the mixture becomes creamy.\r\nAdd the cocoa powder and gradually add the melted chocolate. Finally, the eggs and whiskey are added to the mixture. Stir very lightly with a spatula or wire whisk just to combine. The finished mixture is poured into the baking dish on the biscuit base with butter. The baking form is placed in a larger and deeper tray, into which warm water is poured. The tray is placed in the middle of the oven, which is preheated to 160 °C. Bake for 60 minutes, then leave the cheesecake in the oven with the door slightly ajar until it cools completely. That way it won't drop sharply and crack. Once completely cooled, the cheesecake is wrapped in plastic wrap without removing from the baking pan and placed in the refrigerator overnight to set. For the chocolate ganache, melt 160 g of chocolate (50% cocoa or more) in a water bath and gradually add the cream, which must be at room temperature. Stir carefully with a silicone spatula until the chocolate is completely dissolved in the cream. Finally, add cow butter and stir until it is also absorbed by the mixture. Mix with a blender until the mixture becomes homogeneous and shiny. Set aside to cool slightly. With a sharp knife, carefully run the sides of the baking dish to separate the cheesecake more easily. The ring opens and the walls are removed.\r\nUsing the paper on the bottom and a spatula, the cheesecake is separated from the bottom and transferred to a suitable serving plate. You can use a knife to slightly level the protruding edges of the walls. Pour over the prepared, slightly cooled chocolate ganache, reserving a small amount for decoration. Cover with the lid of the cake plate and place in the refrigerator for an hour. The separated amount of chocolate ganache is transferred to a posh and stored in the refrigerator to harden a little. It is later used to decorate the cheesecake, as you can inject roses or form other decorations to your taste. Finally, the finished chocolate cheesecake is decorated with sugar pearls and fresh fruit of your choice. It is cut into portioned pieces with a dry heated knife, and after each cut the knife must be dried again.",
                Image = File.ReadAllBytes(Path.Combine(@"Images", "Cheesecake.png")),
                CreationDate = DateTime.Parse("26/02/2024"),
                CreatorId = user.Id,
                CategoryId = 1,
            };

            string actualException = recipeService.EditAsync(model).Exception.InnerException.Message;

            Assert.IsTrue(expectedException == actualException);
        }

        [Test]
        public void Test_Edit() 
        {
            string expectedTitle = "Cheesecake 2";
            string expectedDescription = "Have a sweet tooth!!!";
            string expectedInstructions = "The bottom of the pan!!!";

            //Id is 20 because of other test classes otherwise change to 1
            var model = new RecipeFormViewModel()
            {
                Id = 20,
                Title = "Cheesecake 2",
                Description = "Have a sweet tooth!!!",
                Ingredients = "biscuits - 200 g",
                Instructions = "The bottom of the pan!!!",
                Image = File.ReadAllBytes(Path.Combine(@"Images", "Cheesecake.png")),
                CreationDate = DateTime.Parse("25/01/2021"),
                CreatorId = user.Id,
                CategoryId = 1,
            };

            _ = recipeService.EditAsync(model);

            //Id is 20 because of other test classes otherwise change to 1
            var recipe = recipeService.GetByIdAsync(20).Result;

            Assert.IsNotNull(recipe);
            Assert.IsTrue(expectedTitle == recipe.Title);
            Assert.IsTrue(expectedDescription == recipe.Description);
            Assert.IsTrue(expectedInstructions == recipe.Instructions);
        }

        [Test]
        public void Test_GetAllRecipes() 
        {
            //11 because of other test classes otherwise change to 1
            int expectedCount = 11;

            int actualCount = recipeService.GetAllRecipesAsync().Result.Count();

            Assert.IsTrue(expectedCount == actualCount);
        }

        [Test]
        public void Test_GetByIdShouldThrowException()
        {
            string expectedException = "This recipe doesn't exist!";

            string actualException = recipeService.GetByIdAsync(10000).Exception.InnerException.Message;

            Assert.AreEqual(expectedException, actualException);
        }

        [Test]
        public void Test_GetById()
        {
            //Id is 20 because of other test classes otherwise change to 1
            int id = 20;

            string expectedTitle = "Cheesecake";
            string expectedDescription = "Have a sweet tooth!";
            string expectedIngredients = "biscuits - 200 g";
            string expectedInstructions = "The bottom of the pan";
            string expectedCreatorId = "9e59b694-139f-4eb8-91ba-b54ba7fa4b10";
            string expectedCategoryName = "Sweets";
            DateTime expectedCreationDate = DateTime.Parse("25.01.2021");

            var recipe = recipeService.GetByIdAsync(id).Result;

            Assert.NotNull(recipe);
            Assert.IsTrue(expectedTitle == recipe.Title);
            Assert.IsTrue(expectedDescription == recipe.Description);
            Assert.IsTrue(expectedIngredients == recipe.Ingredients);
            Assert.IsTrue(expectedInstructions == recipe.Instructions);
            Assert.IsTrue(expectedCreatorId == recipe.CreatorId);
            Assert.IsTrue(expectedCategoryName == recipe.CategoryName);
            Assert.IsTrue(expectedCreationDate == recipe.CreationDate);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
