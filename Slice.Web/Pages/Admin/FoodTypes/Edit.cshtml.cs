namespace Slice.Web.Pages.Admin.FoodTypes;
public class EditModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public EditModel(IUnitOfWork unitOfWork)
                => _unitOfWork = unitOfWork;

    public async Task OnGetAsync(int id)
        => FoodType = await _unitOfWork.FoodTypeRepository.GetFirstOrDefaultAsync(f => f.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.FoodTypeRepository.Update(FoodType);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Food Type Updated Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
