namespace Slice.Persistence.Repositories;
public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public CartRepository(SliceDbContext context) : base(context)
    {
    }
}