namespace Slice.Persistence.Interfaces;
public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
{
    Task UpdateStatus(int id, string status);
}