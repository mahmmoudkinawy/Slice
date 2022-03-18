namespace Slice.Persistence.Repositories;
public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    private readonly SliceDbContext _context;
    public CartRepository(SliceDbContext context) : base(context)
        => _context = context;

    public async Task<int> DecrementCount(Cart cart, int count)
    {
        cart.Count -= count;
        await _context.SaveChangesAsync();
        return cart.Count;
    }

    public async Task<int> IncrementCount(Cart cart, int count)
    {
        cart.Count += count;
        await _context.SaveChangesAsync();
        return cart.Count;
    }
}