using TasteHub.Core.Services;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.UnitTests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IEnumerable<Comment> comments;
        private ILogger<CommentService> logger;
        private ICommentService commentService;

        [SetUp]
        public void SetUp()
        {
            comments = new List<Comment>() 
            {
                new Comment()
                {
                    Id = 1,
                    Content = "Amazing recipe!",
                    CreationDate = DateTime.Now,
                    UserId = "6131b2c1-b80a-49ec-83ae-51fb006b5c89",
                    RecipeId = 1,
                },
                new Comment()
                {
                    Id = 2,
                    Content = "Well done!",
                    CreationDate = DateTime.Now,
                    UserId = "c208dab4-2a45-43e5-81dd-eb173111575b",
                    RecipeId = 2,
                }
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);
            this.repository.AddRangeAsync<Comment>(this.comments);
            this.repository.SaveChangesAsync();

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<CommentService>();

            commentService = new CommentService(this.repository, logger);
        }
    }
}
