namespace Slice.Web.Pages.Admin.FoodTypes;
public class DeleteModel : PageModel
{
    private readonly IGenericRepository<FoodType> _foodTypeRepository;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public DeleteModel(IGenericRepository<FoodType> foodTypeRepository)
        => _foodTypeRepository = foodTypeRepository;

    public async Task OnGetAsync(int id)
        => FoodType = await _foodTypeRepository.GetFirstOrDefaultAsync(f => f.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        var foodTypeToDelete = await _foodTypeRepository.
            GetFirstOrDefaultAsync(f => f.Id == FoodType.Id);

        if (foodTypeToDelete != null)
        {
            await _foodTypeRepository.Remove(foodTypeToDelete);
            TempData["success"] = "Food Type Deleted Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }

}
