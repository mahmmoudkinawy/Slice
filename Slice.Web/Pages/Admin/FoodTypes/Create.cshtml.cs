namespace Slice.Web.Pages.Admin.FoodTypes;
public class CreateModel : PageModel
{
    private readonly IGenericRepository<FoodType> _foodTypeRepository;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public CreateModel(IGenericRepository<FoodType> foodTypeRepository)
        => _foodTypeRepository = foodTypeRepository;

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _foodTypeRepository.Add(FoodType);
            TempData["success"] = "Food Type Created Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
