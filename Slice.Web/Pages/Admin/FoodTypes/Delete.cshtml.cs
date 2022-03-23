namespace Slice.Web.Pages.Admin.FoodTypes;
public class DeleteModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public DeleteModel(IUnitOfWork unitOfWork)
                => _unitOfWork = unitOfWork;

    public async Task OnGetAsync(int id)
        => FoodType = await _unitOfWork.FoodTypeRepository.GetFirstOrDefaultAsync(f => f.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        var foodTypeToDelete = await _unitOfWork.FoodTypeRepository.
            GetFirstOrDefaultAsync(f => f.Id == FoodType.Id);

        if (foodTypeToDelete != null)
        {
            await _unitOfWork.FoodTypeRepository.Remove(foodTypeToDelete);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Food Type Deleted Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }

}
