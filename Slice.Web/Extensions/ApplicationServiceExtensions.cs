namespace Slice.Web.Extensions;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddRazorPages();

        services.AddDbContext<SliceDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPhotoService, PhotoService>();

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}