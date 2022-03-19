namespace Slice.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly SliceDbContext _context;

    public UnitOfWork(SliceDbContext context)
    {
        _context = context;
        CategoryRepository = new GenericRepository<Category>(_context);
        FoodTypeRepository = new GenericRepository<FoodType>(_context);
        ProductRepository = new GenericRepository<Product>(_context);
    }

    public IGenericRepository<Category> CategoryRepository { get; private set; }

    public IGenericRepository<FoodType> FoodTypeRepository { get; private set; }
    public IGenericRepository<Product> ProductRepository { get; private set; }
}