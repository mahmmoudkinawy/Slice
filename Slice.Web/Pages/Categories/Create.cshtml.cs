namespace Slice.Web.Pages.Categories;
public class CreateModel : PageModel
{
    private readonly SliceDbContext _context;

    [BindProperty]
    public Category Category { get; set; }

    public CreateModel(SliceDbContext context) => _context = context;

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category.Name.Equals(Category.DisplayOrder.ToString()))
            ModelState.AddModelError("Category.Name", "Name and Display Order can not be the same");

        if (ModelState.IsValid)
        {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Created Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
