namespace Slice.Persistence.Interfaces;
public interface ICartRepository : IGenericRepository<Cart>
{
    Task<int> IncrementCount(Cart cart, int count);
    Task<int> DecrementCount(Cart cart, int count);
}