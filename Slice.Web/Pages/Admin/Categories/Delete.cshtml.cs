namespace Slice.Web.Pages.Admin.Categories;
public class DeleteModel : PageModel
{
    private readonly SliceDbContext _context;

    [BindProperty]
    public Category Category { get; set; }

    public DeleteModel(SliceDbContext context) => _context = context;

    public async Task OnGetAsync(int id)
        => Category = await _context.Categories.FindAsync(id);

    public async Task<IActionResult> OnPostAsync()
    {
        var categoryToDelete = await _context.Categories.FindAsync(Category.Id);
        if (categoryToDelete != null)
        {
            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }

}
