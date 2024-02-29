using System.Linq.Expressions;

namespace TasteHub.Infrastructure.Common
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> AllAsync<T>() where T : class;
        IQueryable<T> AllAsync<T>(Expression<Func<T,bool>> search) where T : class;          
        IQueryable<T> AllReadonlyAsync<T>() where T : class;
        IQueryable<T> AllReadonlyAsync<T>(Expression<Func<T,bool>> search) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task<T?> GetByIdAsync<T>(object id) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task DeleteRange<T>(IEnumerable<T> entities) where T : class;
        Task<int> SaveChangesAsync();
    }
}
