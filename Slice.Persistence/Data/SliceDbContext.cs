﻿namespace Slice.Persistence.Data;
public class SliceDbContext : IdentityDbContext
{
    public SliceDbContext(DbContextOptions<SliceDbContext> options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<FoodType> FoodTypes { get; set; }
    public DbSet<Product> Products { get; set; }
}