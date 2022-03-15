var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<SliceDbContext>();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
