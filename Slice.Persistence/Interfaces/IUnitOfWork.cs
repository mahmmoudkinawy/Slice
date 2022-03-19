namespace Slice.Persistence.Interfaces;
public interface IUnitOfWork
{
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<FoodType> FoodTypeRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
}
