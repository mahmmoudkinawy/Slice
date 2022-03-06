namespace Slice.Web.Pages.Admin.FoodTypes;
public class IndexModel : PageModel
{
    private readonly SliceDbContext _context;
    public IReadOnlyList<FoodType> FoodTypes { get; set; }

    public IndexModel(SliceDbContext context) => _context = context;

    public async Task OnGet()
        => FoodTypes = await _context.FoodTypes.ToListAsync();
}
