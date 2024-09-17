using System.Linq.Expressions;

namespace BankingManagementSystem.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindAsync(int id);
        
        Task<T> FindAsync(string id);

        Task<List<T>> ToListAsync();

        Task AddAsync(T entity);

        void Update(T entity);

        void Remove(T entity);

        Task SaveChangesAsync();

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    }
}