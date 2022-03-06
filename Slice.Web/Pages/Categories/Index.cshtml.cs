namespace Slice.Web.Pages.Categories;
public class IndexModel : PageModel
{
    private readonly SliceDbContext _context;
    public IReadOnlyList<Category> Categories { get; set; }

    public IndexModel(SliceDbContext context) => _context = context;

    public async Task OnGet()
        => Categories = await _context.Categories.ToListAsync();
}
