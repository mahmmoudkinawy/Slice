﻿namespace Slice.Persistence.Interfaces;
public interface IUnitOfWork
{
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<FoodType> FoodTypeRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<OrderDetail> OrderDetailRepository { get; }
    IGenericRepository<AppUser> AppUserRepository { get; } //I know this is a bad Idea, but I will try to refactor it.
    IOrderHeaderRepository OrderHeaderRepository { get; }
    ICartRepository CartRepository { get; }

    //I know that I'm breaking Single Responsiblity
    Task SaveChangesAsync();
}
