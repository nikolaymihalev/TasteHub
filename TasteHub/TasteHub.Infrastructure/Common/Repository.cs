using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TasteHub.Infrastructure.Data;

namespace TasteHub.Infrastructure.Common
{
    /// <summary>
    /// Repository class with methods for Relational Database Engine
    /// </summary>
    public class Repository : IRepository
    {
        /// <summary>
        /// Entity framework Db context
        /// </summary>
        protected DbContext Context { get; set; }

        /// <summary>
        /// Table in database
        /// </summary>
        protected DbSet<T> DbSet<T>() where T : class
        {
            return this.Context.Set<T>();
        }

        public Repository(ApplicationDbContext _context)
        {
            Context = _context;
        }

        /// <summary>
        /// All data in a table
        /// </summary>
        public IEnumerable<T> All<T>() where T : class
        {
            return this.DbSet<T>().AsQueryable();
        }

        /// <summary>
        /// All data in a table
        /// </summary>
        public IEnumerable<T> All<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>().Where(search).AsQueryable();
        }

        /// <summary>
        /// All data as no tracking in a table
        /// </summary>
        public IEnumerable<T> AllReadonly<T>() where T : class        
        {
            return this.DbSet<T>().AsNoTracking();
        }

        /// <summary>
        /// All data as no tracking in a table
        /// </summary>
        public IEnumerable<T> AllReadonly<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>().Where(search).AsNoTracking();
        }

        /// <summary>
        /// Add entity to the database
        /// </summary>
        public async Task AddAsync<T>(T entity) where T : class
        {
            await this.DbSet<T>().AddAsync(entity);
        }

        /// <summary>
        /// Get specific entity from database by identifier
        /// </summary>
        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }

        /// <summary>
        /// Delete entity from database
        /// </summary>
        public async Task DeleteAsync<T>(object id) where T : class
        {
            T? entity = await GetByIdAsync<T>(id);

            if(entity != null)
                Delete<T>(entity);
        }

        /// <summary>
        /// Delete entity from database
        /// </summary>
        public void Delete<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet<T>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Delete range of entities from database
        /// </summary>
        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Save changes in database
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposing context when it is not needed
        /// </summary>
        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}