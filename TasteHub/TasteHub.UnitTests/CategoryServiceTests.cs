using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.UnitTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IEnumerable<Category> categories;
        private ILogger<CategoryService> logger;
        private ICategoryService categoryService;

        [SetUp]
        public void SetUp()
        {
            this.categories = new List<Category>()
            {
                new Category(){ Id = 1, Name = "Sweets" },
                new Category(){ Id = 2, Name = "Sandwiches" }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()                
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);
            
            this.repository = new Repository(this.context);
            this.repository.AddRangeAsync<Category>(this.categories);
            this.repository.SaveChangesAsync();

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<CategoryService>();

            categoryService = new CategoryService(this.repository, logger);
        }

        [Test]
        public void Test_GetAllCategories()
        {
            int expectedCount = 2;
            string expectedFirstCategoryName = "Sweets";
            string expectedSecondCategoryName = "Sandwiches";
            int expectedSecondCategoryId = 2;


            var actualCategories = categoryService.GetAllCategoriesAsync().Result.ToList();

            Assert.AreEqual(expectedCount, actualCategories.Count());
            Assert.AreEqual(expectedFirstCategoryName, actualCategories[0].Name);
            Assert.AreEqual(expectedSecondCategoryName, actualCategories[1].Name);
            Assert.AreEqual(expectedSecondCategoryId, actualCategories[1].Id);
        }

        [Test]
        public void Test_GetByIdShouldReturnNull()
        {
            string result = "";
            CategoryInfoViewModel? category = null;

            try
            {
                category = categoryService.GetByIdAsync(3).Result;

            }
            catch (Exception ex)
            {
                result = ex.InnerException.Message;
            }

            Assert.IsNull(category);
            Assert.AreEqual("This category doesn't exist!", result);
        }

        [Test]
        public void Test_GetById()
        {
            int expectedId = 2;
            string expectedName = "Sandwiches";

            var category = categoryService.GetByIdAsync(2).Result;

            Assert.IsTrue(category != null);
            Assert.AreEqual(expectedId, category.Id);
            Assert.AreEqual(expectedName, category.Name);
        }

        [Test]
        public void Test_GetByNameShouldReturnNull()
        {
            string result = "";
            CategoryInfoViewModel? category = null;

            try
            {
                category = categoryService.GetByNameAsync("Pizza").Result;

            }
            catch (Exception ex)
            {
                result = ex.InnerException.Message;
            }

            Assert.IsNull(category);
            Assert.AreEqual("This category doesn't exist!", result);
        }

        [Test]
        public void Test_GetByName() 
        {
            int expectedId = 1;
            string expectedName = "Sweets";

            var category = categoryService.GetByNameAsync("Sweets").Result;

            Assert.IsTrue(category != null);
            Assert.AreEqual(expectedId, category.Id);
            Assert.AreEqual(expectedName, category.Name);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
