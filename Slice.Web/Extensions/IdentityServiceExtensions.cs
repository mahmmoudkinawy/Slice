namespace Slice.Web.Extensions;
public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<SliceDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        services.AddSingleton<IEmailSender, EmailSender>();

        return services;
    }
}