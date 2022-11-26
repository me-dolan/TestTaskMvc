using System.Linq.Expressions;

namespace TestTaskMvc.Data.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(int? id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        Task SaveAsync();
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true);
    }
}
