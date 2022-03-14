namespace Slice.Persistence.Interfaces;
public interface IGenericRepository<T> : IDisposable where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeProperties = null);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null);
    Task Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
    Task RemoveRange(IEnumerable<T> entities);
}
