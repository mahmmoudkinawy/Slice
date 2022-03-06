namespace Slice.Persistence.Data;
public class SliceDbContext : DbContext
{
    public SliceDbContext(DbContextOptions<SliceDbContext> options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
}