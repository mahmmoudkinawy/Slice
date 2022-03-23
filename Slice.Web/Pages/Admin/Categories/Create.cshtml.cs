namespace Slice.Web.Pages.Admin.Categories;
public class CreateModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public Category Category { get; set; }

    public CreateModel(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category.Name.Equals(Category.DisplayOrder.ToString()))
            ModelState.AddModelError("Category.Name", "Name and Display Order can not be the same");

        if (ModelState.IsValid)
        {
            await _unitOfWork.CategoryRepository.Add(Category);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Category Created Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
