namespace Slice.Web.Pages.Admin.Categories;
public class DeleteModel : PageModel
{
    private readonly IGenericRepository<Category> _categoryRepository;

    [BindProperty]
    public Category Category { get; set; }

    public DeleteModel(IGenericRepository<Category> categoryRepository)
        => _categoryRepository = categoryRepository;

    public async Task OnGetAsync(int id)
        => Category = await _categoryRepository.GetFirstOrDefaultAsync(c => c.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        var categoryToDelete = await _categoryRepository
            .GetFirstOrDefaultAsync(c => c.Id == Category.Id);

        if (categoryToDelete != null)
        {
            await _categoryRepository.Remove(categoryToDelete);
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }

}
