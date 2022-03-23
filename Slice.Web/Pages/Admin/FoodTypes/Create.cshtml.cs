namespace Slice.Web.Pages.Admin.FoodTypes;
public class CreateModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public CreateModel(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.FoodTypeRepository.Add(FoodType);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Food Type Created Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
