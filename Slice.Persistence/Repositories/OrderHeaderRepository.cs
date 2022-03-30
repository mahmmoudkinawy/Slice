namespace Slice.Persistence.Repositories;
public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
{
    private readonly SliceDbContext _context;

    public OrderHeaderRepository(SliceDbContext context) : base(context) => _context = context;

    public async Task UpdateStatus(int id, string status)
    {
        var orderHeaderFromDb = await _context.OrderHeaders.FindAsync(id);
        if (orderHeaderFromDb != null)
        {
            orderHeaderFromDb.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}