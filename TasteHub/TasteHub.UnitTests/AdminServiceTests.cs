using TasteHub.Core.Models.Admin;

namespace TasteHub.UnitTests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IdentityUser user1;
        private IdentityUser user2;
        private AdminQuery query;
        private ILogger<AdminService> logger;
        private IAdminService adminService;

        [SetUp]
        public void SetUp()
        {
            user1 = new IdentityUser()
            {
                Id = "0aac6bf1-1870-48ad-9c0a-cfb0dd3f9566",
                UserName = "firstUser@mail.com",
                NormalizedUserName = "firstUser@mail.com",
                Email = "firstUser@mail.com",
                NormalizedEmail = "firstUser@mail.com"
            };
            
            user2 = new IdentityUser()
            {
                Id = "da971740-b838-4149-ad81-d8e56ba80540",
                UserName = "secondUser@mail.com",
                NormalizedUserName = "secondUser@mail.com",
                Email = "secondUser@mail.com",
                NormalizedEmail = "secondUser@mail.com"
            };

            query = new AdminQuery()
            {
                Id = 1,
                UserId = user1.Id,
                Description = "I want to be an Admin"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TasteHubDb")
                .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync<IdentityUser>(user1);
            this.repository.AddAsync<IdentityUser>(user2);
            this.repository.AddAsync<AdminQuery>(query);
            this.repository.SaveChangesAsync();

            var loggerFactory = new LoggerFactory();
            this.logger = loggerFactory.CreateLogger<AdminService>();

            adminService = new AdminService(this.repository, logger);
        }

        [Test]
        public void Test_Add()
        {
            int expectedCount = 2;

            var model = new QueryFormModel()
            {
                UserId = user2.Id,
                Description = "I want to be an Admin too"
            };

            _ = adminService.AddAsync(model);

            int actualCount = adminService.GetAllQueriesAsync().Result.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
