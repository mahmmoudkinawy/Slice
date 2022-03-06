namespace Slice.Web.Pages.Admin.FoodTypes;
public class CreateModel : PageModel
{
    private readonly SliceDbContext _context;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public CreateModel(SliceDbContext context) => _context = context;

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            _context.FoodTypes.Add(FoodType);
            await _context.SaveChangesAsync();
            TempData["success"] = "Food Type Created Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
