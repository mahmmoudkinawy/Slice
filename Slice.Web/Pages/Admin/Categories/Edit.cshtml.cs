namespace Slice.Web.Pages.Admin.Categories;
public class EditModel : PageModel
{
    private readonly SliceDbContext _context;

    [BindProperty]
    public Category Category { get; set; }

    public EditModel(SliceDbContext context) => _context = context;

    public async Task OnGetAsync(int id)
        => Category = await _context.Categories.FindAsync(id);

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category.Name.Equals(Category.DisplayOrder.ToString()))
            ModelState.AddModelError("Category.Name", "Name and Display Order can not be the same");

        if (ModelState.IsValid)
        {
            _context.Categories.Update(Category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
