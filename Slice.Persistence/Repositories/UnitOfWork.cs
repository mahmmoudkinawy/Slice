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
        AppUserRepository = new GenericRepository<AppUser>(_context);
        OrderDetailRepository = new GenericRepository<OrderDetail>(_context);
        OrderHeaderRepository = new OrderHeaderRepository(_context);
        CartRepository = new CartRepository(_context);
    }

    public IGenericRepository<Category> CategoryRepository { get; private set; }
    public IGenericRepository<FoodType> FoodTypeRepository { get; private set; }
    public IGenericRepository<Product> ProductRepository { get; private set; }
    public IGenericRepository<AppUser> AppUserRepository { get; private set; }
    public IOrderHeaderRepository OrderHeaderRepository { get; private set; }
    public IGenericRepository<OrderDetail> OrderDetailRepository { get; private set; }
    public ICartRepository CartRepository { get; private set; }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}