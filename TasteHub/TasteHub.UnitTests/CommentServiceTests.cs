using TasteHub.Core.Models.Comment;
using TasteHub.Core.Services;

namespace TasteHub.UnitTests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IEnumerable<Comment> comments;
        private IdentityUser user;
        private Recipe recipe;
        private ILogger<CommentService> logger;
        private ICommentService commentService;

        [SetUp]
        public void SetUp()
        {
            user = new IdentityUser()
            {
                Id = "c208dab4-2a45-43e5-81dd-eb173111575b",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };

            recipe = new Recipe() 
            {
                Id = 1,
                Title = "Chocolate cheesecake",
                Description = "Have a sweet tooth with my special occasion chocolate cheesecake!",
                Ingredients = "biscuits - 200 g, cocoa; cow butter - 60 g; cream cheese - 500 g; chocolate - 160 g (50% cocoa); sugar - 100 g, brown; eggs - 2 pcs.; whiskey - 20 ml; FOR THE CHOCOLATE GANACHE; chocolate - 160 g (50% cocoa); cream - 160 g confectionery (30% fat); cow butter - 50 g; FOR DECORATION; fresh fruit - optional; sugar pearls",
                Instructions = "The bottom of the pan, in which we will prepare the chocolate cheesecake, is lined with baking paper. On the outside, it is covered with two layers of aluminum foil, and the goal is to prevent water from entering the mold during baking, as it will be in a water bath. It is important to note that if you are going to make walls of the chocolate cheesecake from the biscuit mixture, it is good that the walls of the pan can be removed. If you are not going to make walls, but only a base, then the amount of biscuits should be reduced to 120 g. Cocoa biscuits are crushed in a chopper and then mixed with melted cow butter, mixed until homogeneous. The biscuit mixture is poured into the prepared baking form. If walls are also being made for the cheesecake, shape the base with the back of a spoon and start with the walls first and finish with the bottom. The form is stored in the refrigerator. For the cheesecake mixture, first, melt the chocolate in a water bath and set it aside. Mix the cream cheese, which should be at room temperature, with the brown sugar and beat with a mixer until the sugar dissolves and the mixture becomes creamy.\r\nAdd the cocoa powder and gradually add the melted chocolate. Finally, the eggs and whiskey are added to the mixture. Stir very lightly with a spatula or wire whisk just to combine. The finished mixture is poured into the baking dish on the biscuit base with butter. The baking form is placed in a larger and deeper tray, into which warm water is poured. The tray is placed in the middle of the oven, which is preheated to 160 °C. Bake for 60 minutes, then leave the cheesecake in the oven with the door slightly ajar until it cools completely. That way it won't drop sharply and crack. Once completely cooled, the cheesecake is wrapped in plastic wrap without removing from the baking pan and placed in the refrigerator overnight to set. For the chocolate ganache, melt 160 g of chocolate (50% cocoa or more) in a water bath and gradually add the cream, which must be at room temperature. Stir carefully with a silicone spatula until the chocolate is completely dissolved in the cream. Finally, add cow butter and stir until it is also absorbed by the mixture. Mix with a blender until the mixture becomes homogeneous and shiny. Set aside to cool slightly. With a sharp knife, carefully run the sides of the baking dish to separate the cheesecake more easily. The ring opens and the walls are removed.\r\nUsing the paper on the bottom and a spatula, the cheesecake is separated from the bottom and transferred to a suitable serving plate. You can use a knife to slightly level the protruding edges of the walls. Pour over the prepared, slightly cooled chocolate ganache, reserving a small amount for decoration. Cover with the lid of the cake plate and place in the refrigerator for an hour. The separated amount of chocolate ganache is transferred to a posh and stored in the refrigerator to harden a little. It is later used to decorate the cheesecake, as you can inject roses or form other decorations to your taste. Finally, the finished chocolate cheesecake is decorated with sugar pearls and fresh fruit of your choice. It is cut into portioned pieces with a dry heated knife, and after each cut the knife must be dried again.",
                Image = File.ReadAllBytes(Path.Combine(@"D:\Projects\Apps\TasteHub\TasteHub\TasteHub\TasteHub.Infrastructure\Images", "Cheesecake.png")),
                CreationDate = DateTime.Now,
                CreatorId = user.Id,
                CategoryId = 2,
            };

            comments = new List<Comment>() 
            {
                new Comment()
                {
                    Id = 1,
                    Content = "Amazing recipe!",
                    CreationDate = DateTime.Parse("25-02-2024"),
                    UserId = user.Id,
                    RecipeId = recipe.Id,
                },
                new Comment()
                {
                    Id = 2,
                    Content = "Well done!",
                    CreationDate = DateTime.Parse("29-02-2024"),
                    UserId = user.Id,
                    RecipeId = recipe.Id,
                }
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddRangeAsync<Comment>(this.comments);
            this.repository.AddAsync<Recipe>(recipe);
            this.repository.AddAsync<IdentityUser>(user);
            this.repository.SaveChangesAsync();

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<CommentService>();

            commentService = new CommentService(this.repository, logger);
        }

        [Test]
        public void Test_GetLastCommentForRecipe() 
        {
            string expectedContent = "Well done!";
            string expectedUsername = "guest@mail.com";
            var expectedCreationDate = DateTime.Parse("29-02-2024");
            int expectedId = 2;
            string expectedUserId = "c208dab4-2a45-43e5-81dd-eb173111575b";
            int expectedRecipeId = 1;
            string expectedRecipeTitle = "Chocolate cheesecake";

            var comment = commentService.GetLastCommentAboutRecipeAsync(1).Result;

            Assert.AreEqual(expectedContent, comment.Content);
            Assert.AreEqual(expectedUsername, comment.UserUsername);
            Assert.AreEqual(expectedCreationDate, comment.CreationDate);
            Assert.AreEqual(expectedId, comment.Id);
            Assert.AreEqual(expectedUserId, comment.UserId);
            Assert.AreEqual(expectedRecipeId, comment.RecipeId);
            Assert.AreEqual(expectedRecipeTitle, comment.RecipeTitle);
        }

        [Test]
        public void Test_GetCommentsByRecipe() 
        {
            int expectedCount = 2;

            int actualCount = commentService.GetAllCommentsAboutRecipeAsync(1).Result.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Test_GetByIdShouldThrowException() 
        {
            string result = string.Empty;
            CommentInfoModel? comment = null;

            try
            {
                comment = commentService.GetByIdAsync(25).Result;
            }
            catch (Exception ex)
            {
                result = ex.InnerException.Message;
            }

            Assert.IsNull(comment);
            Assert.AreEqual("This comment doesn't exist!", result);
        }

        [Test]
        public void Test_GetById() 
        {
            int expectedId = 1;
            string expectedContent = "Amazing recipe!";
            string expectedUserId = "c208dab4-2a45-43e5-81dd-eb173111575b";
            int expectedRecipeId = 1;
            DateTime expectedDate = DateTime.Parse("25-02-2024");

            var comment = commentService.GetByIdAsync(1).Result;

            Assert.IsTrue(comment != null);
            Assert.IsTrue(expectedId == comment.Id);
            Assert.IsTrue(expectedContent == comment.Content);
            Assert.IsTrue(expectedUserId == comment.UserId);
            Assert.IsTrue(expectedRecipeId == comment.RecipeId);
            Assert.IsTrue(expectedDate == comment.CreationDate);
            Assert.IsTrue(comment.RecipeTitle == string.Empty);
            Assert.IsTrue(comment.UserUsername == string.Empty);
        }
    }
}
