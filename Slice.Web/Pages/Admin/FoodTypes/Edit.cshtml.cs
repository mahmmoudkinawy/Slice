namespace Slice.Web.Pages.Admin.FoodTypes;
public class EditModel : PageModel
{
    private readonly IGenericRepository<FoodType> _foodTypeRepository;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public EditModel(IGenericRepository<FoodType> foodTypeRepository)
        => _foodTypeRepository = foodTypeRepository;

    public async Task OnGetAsync(int id)
        => FoodType = await _foodTypeRepository.GetFirstOrDefaultAsync(f => f.Id == id);

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _foodTypeRepository.Update(FoodType);
            TempData["success"] = "Food Type Updated Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
