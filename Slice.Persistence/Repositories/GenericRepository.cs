namespace Slice.Persistence.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly SliceDbContext _context;

    public GenericRepository(SliceDbContext context) => _context = context;

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
            query = query.Where(filter);

        return await query.FirstOrDefaultAsync();
    }

    public async Task Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

