namespace Slice.Web.Pages.Admin.Categories;
public class EditModel : PageModel
{
    private readonly IGenericRepository<Category> _categoryRepository;

    [BindProperty]
    public Category Category { get; set; }

    public EditModel(IGenericRepository<Category> categoryRepository)
        => _categoryRepository = categoryRepository;

    public async Task OnGetAsync(int id)
        => Category = await _categoryRepository.GetFirstOrDefaultAsync(c => c.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category.Name.Equals(Category.DisplayOrder.ToString()))
            ModelState.AddModelError("Category.Name", "Name and Display Order can not be the same");

        if (ModelState.IsValid)
        {
            await _categoryRepository.Update(Category);
            TempData["success"] = "Category Updated Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
