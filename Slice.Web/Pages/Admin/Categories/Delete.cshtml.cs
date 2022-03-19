namespace Slice.Web.Pages.Admin.Categories;
public class DeleteModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public Category Category { get; set; }

    public DeleteModel(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task OnGetAsync(int id)
        => Category = await _unitOfWork.CategoryRepository.GetFirstOrDefaultAsync(c => c.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        var categoryToDelete = await _unitOfWork.CategoryRepository
            .GetFirstOrDefaultAsync(c => c.Id == Category.Id);

        if (categoryToDelete != null)
        {
            await _unitOfWork.CategoryRepository.Remove(categoryToDelete);
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }

}
