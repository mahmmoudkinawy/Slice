namespace Slice.Persistence.Interfaces;
public interface IGenericRepository<T> : IDisposable where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null);
    Task Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
    Task RemoveRange(IEnumerable<T> entities);
}
