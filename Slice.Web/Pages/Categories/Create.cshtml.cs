namespace Slice.Web.Pages.Categories;
public class CreateModel : PageModel
{
    private readonly SliceDbContext _context;

    [BindProperty]
    public Category Category { get; set; }

    public CreateModel(SliceDbContext context) => _context = context;

    public async Task<IActionResult> OnPostAsync()
    {

        if (ModelState.IsValid)
        {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        return Page();
    }

}
