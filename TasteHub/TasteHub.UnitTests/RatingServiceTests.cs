using Newtonsoft.Json.Linq;
using TasteHub.Core.Models.Rating;
using TasteHub.Core.Services;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.UnitTests
{
    [TestFixture]
    public class RatingServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private Rating rating;
        private IdentityUser user;
        private IdentityUser guest;
        private IdentityUser tester;
        private Recipe recipe;
        private Category category;
        private ILogger<RatingService> logger;
        private IRatingService ratingService;

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

            guest = new IdentityUser()
            {
                Id = "a8b74cb2-a1e8-488e-85ef-c714a85c072d",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };

            tester = new IdentityUser()
            {
                Id = "556ff379-8109-4dad-acf7-56a4b586932a",
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
                Name = "Noodles"
            };

            rating = new Rating()
            {
                Id = 1,
                RecipeId = 1,
                UserId = guest.Id,
                Value = 5
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync<Rating>(this.rating);
            this.repository.AddAsync<Recipe>(recipe);
            this.repository.AddAsync<Category>(category);
            this.repository.AddAsync<IdentityUser>(user);
            this.repository.AddAsync<IdentityUser>(guest);
            this.repository.AddAsync<IdentityUser>(tester);
            this.repository.SaveChangesAsync();

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<RatingService>();

            ratingService = new RatingService(this.repository, logger);
        }

        [Test]
        public void Test_Add()
        {
            int expectedCount = 2;

            var model = new RatingFormModel()
            {
                RecipeId = 1,
                UserId = tester.Id,
                Value = 3
            };

            _ = ratingService.AddAsync(model);

            int actualCount = ratingService.GetAllRatingsAboutRecipeAsync(1).Result.Count();

            Assert.IsTrue(expectedCount == actualCount);
        }

        [Test]
        public void Test_AddShouldThrowException()
        {
            string expectedException = "Operation failed. Try again!";

            var model = new RatingFormModel()
            {
                RecipeId = 1,
                UserId = guest.Id,
                Value = 5
            };

            string actualException = ratingService.AddAsync(model).Exception.InnerException.Message;

            Assert.IsTrue(expectedException == actualException);
        }

        [Test]
        public void Test_DeleteShouldThrowException()
        {
            string expectedException = "Invalid rating!";

            string actualException = ratingService.DeleteAsync(1000).Exception.InnerException.Message;

            Assert.IsTrue(expectedException == actualException);
        }

        [Test]
        public void Test_GetAllRatingsAboutRecipe()
        {
            int expectedCount = 2;

            int actualCount = ratingService.GetAllRatingsAboutRecipeAsync(1).Result.Count();

            Assert.IsTrue(expectedCount == actualCount);
        }

        [Test]
        public void Test_GetAverageRatingAboutRecipe() 
        {
            double expectedValue = 4;

            double actualValue = ratingService.GetAverageRatingAboutRecipeAsync(1).Result;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void Test_GetAverageRatingShouldReturnZero() 
        {
            double expectedValue = 0;

            double actualValue = ratingService.GetAverageRatingAboutRecipeAsync(1000).Result;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void Test_EditShouldThrowException()
        {
            string expectedException = "Invalid rating!";

            var model = new RatingFormModel()
            {
                RecipeId = 100000,
                UserId = "invalidid",
                Value = 2
            };

            string actualException = ratingService.EditAsync(model).Exception.InnerException.Message;

            Assert.IsTrue(expectedException == actualException);
        }

        [Test]
        public void Test_Edit()
        {
            double expectedValue = 5;

            var model = new RatingFormModel()
            {
                RecipeId = 1,
                UserId = guest.Id,
                Value = 5
            };

            _ = ratingService.EditAsync(model);
            var lastRatingAboutRecipe = ratingService.GetAllRatingsAboutRecipeAsync(1).Result
                .ToList()
                .FirstOrDefault(x=>x.RecipeId == model.RecipeId && x.UserId == model.UserId);

            Assert.IsTrue(expectedValue == lastRatingAboutRecipe.Value);
        }

        [Test]
        public void Test_Delete()
        {
            int expectedCount = 2;

            var model = new RatingFormModel()
            {
                RecipeId = 1,
                UserId = tester.Id,
                Value = 1
            };

            _ = ratingService.AddAsync(model);
            _ = ratingService.DeleteAsync(3);

            int actualCount = ratingService.GetAllRatingsAboutRecipeAsync(1).Result.Count();

            Assert.IsTrue(expectedCount == actualCount);

        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
