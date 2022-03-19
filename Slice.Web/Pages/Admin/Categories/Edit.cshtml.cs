namespace Slice.Web.Pages.Admin.Categories;
public class EditModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public Category Category { get; set; }

    public EditModel(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task OnGetAsync(int id)
        => Category = await _unitOfWork.CategoryRepository.GetFirstOrDefaultAsync(c => c.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        if (Category.Name.Equals(Category.DisplayOrder.ToString()))
            ModelState.AddModelError("Category.Name", "Name and Display Order can not be the same");

        if (ModelState.IsValid)
        {
            await _unitOfWork.CategoryRepository.Update(Category);
            TempData["success"] = "Category Updated Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
