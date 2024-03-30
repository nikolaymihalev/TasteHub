namespace TasteHub.UnitTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IEnumerable<CategoryInfoViewModel> categories;
        private ILogger<CategoryService> logger;
        private ICategoryService categoryService;

        [SetUp]
        public void SetUp()
        {
            this.categories = new List<CategoryInfoViewModel>()
            {
                new CategoryInfoViewModel(1,"Sweets"),
                new CategoryInfoViewModel(2, "Sandwiches")
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()                
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);
            this.repository = new Repository(this.context);
            this.repository.AddRangeAsync(this.categories);

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<CategoryService>();

            categoryService = new CategoryService(this.repository, logger);
        }

        [Test]
        public void Test_GetAllCategories()
        {
            var expectedCount = 2;
            var expectedFirstCategoryName = "Sweets";
            var expectedSecondCategoryName = "Sandwiches";
            var expectedSecondCategoryId = 2;


            var actualCategories = categoryService.GetAllCategoriesAsync().Result.ToList();

            Assert.AreEqual(expectedCount, actualCategories.Count());
            Assert.AreEqual(expectedFirstCategoryName, actualCategories[0].Name);
            Assert.AreEqual(expectedSecondCategoryName, actualCategories[1].Name);
            Assert.AreEqual(expectedSecondCategoryId, actualCategories[1].Id);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
