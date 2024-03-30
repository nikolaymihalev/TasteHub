using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TasteHub.Core.Models.Category;
using TasteHub.Infrastructure.Data;

namespace TasteHub.UnitTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private ApplicationDbContext context;
        private IEnumerable<CategoryInfoViewModel> categories;

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
            this.context.AddRange(this.categories);
            this.context.SaveChanges();
        }
    }
}
