namespace TasteHub.UnitTests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IdentityUser user1;
        private IdentityUser user2;
        private IdentityRole role;
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

            role = new IdentityRole()
            {
                NormalizedName = "Admin",
                Name = "Admin"
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
            this.repository.AddAsync<IdentityRole>(role);
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

        [Test]
        public void Test_GetAllQueries()
        {
            int expectedCount = 2;

            int actualCount = adminService.GetAllQueriesAsync().Result.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Test_QueryExistsShouldReturnTrue() 
        {
            Assert.IsTrue(adminService.QueryExistsAsync(1).Result);
        }
        
        [Test]
        public void Test_QueryExistsShouldReturnFalse() 
        {
            Assert.IsFalse(adminService.QueryExistsAsync(100000).Result);
        }

        [Test]
        public void Test_RemoveQuery() 
        {
            int expectedCount = 2;

            var model = new QueryFormModel()
            {
                UserId = user1.Id,
                Description = "I want to be an Admin because it is very interesting"
            };

            _ = adminService.AddAsync(model);

            _ = adminService.RemoveAsync(3);

            int actualCount = adminService.GetAllQueriesAsync().Result.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Test_RemoveQueryShouldThrowException()
        {
            string expectedException = "Invalid query!";

            string actualException = adminService.RemoveAsync(1000000).Exception.InnerException.Message;

            Assert.AreEqual(expectedException, actualException);
        }

        [Test]
        public void Test_GetAllRoles()
        {
            int expectedCount = 1;

            int actualCount = adminService.GetAllRolesAsync().Result.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Test_UserExistsShouldReturnTrue() 
        {
            Assert.IsTrue(adminService.UserExistsAsync("da971740-b838-4149-ad81-d8e56ba80540").Result);
        }
        
        [Test]
        public void Test_UserExistsShouldReturnFalse() 
        {
            Assert.IsFalse(adminService.UserExistsAsync("InvalidIdentifer").Result);
        }

        [Test]
        public void Test_GetAllUsers() 
        {
            int expectedCount = 2;

            int actualCount = adminService.GetAllUsersAsync().Result.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
