namespace Slice.Web.Pages.Admin.Categories;
public class CreateModel : PageModel
{
    private readonly IGenericRepository<Category> _categoryRepository;

    [BindProperty]
    public Category Category { get; set; }

    public CreateModel(IGenericRepository<Category> categoryRepository) 
        => _categoryRepository = categoryRepository;

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category.Name.Equals(Category.DisplayOrder.ToString()))
            ModelState.AddModelError("Category.Name", "Name and Display Order can not be the same");

        if (ModelState.IsValid)
        {
            await _categoryRepository.Add(Category);
            TempData["success"] = "Category Created Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
