namespace Slice.Web.Pages.Admin.FoodTypes;
public class DeleteModel : PageModel
{
    private readonly SliceDbContext _context;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public DeleteModel(SliceDbContext context) => _context = context;

    public async Task OnGetAsync(int id)
        => FoodType = await _context.FoodTypes.FindAsync(id);

    public async Task<IActionResult> OnPostAsync()
    {
        var foodTypeToDelete = await _context.FoodTypes.FindAsync(FoodType.Id);
        if (foodTypeToDelete != null)
        {
            _context.FoodTypes.Remove(foodTypeToDelete);
            await _context.SaveChangesAsync();
            TempData["success"] = "Food Type Deleted Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }

}
