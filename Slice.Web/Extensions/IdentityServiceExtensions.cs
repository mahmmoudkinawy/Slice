namespace Slice.Web.Extensions;
public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<SliceDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        services.AddSingleton<IEmailSender, EmailSender>();

        services.AddAuthentication().AddFacebook(options =>
        {
            options.AppId = config.GetValue<string>("Facebook:AppId");
            options.AppSecret = config.GetValue<string>("Facebook:AppSecret");
        });

        return services;
    }
}