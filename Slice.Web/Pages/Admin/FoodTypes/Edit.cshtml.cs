namespace Slice.Web.Pages.Admin.FoodTypes;
public class EditModel : PageModel
{
    private readonly SliceDbContext _context;

    [BindProperty]
    public FoodType FoodType { get; set; }

    public EditModel(SliceDbContext context) => _context = context;

    public async Task OnGetAsync(int id)
        => FoodType = await _context.FoodTypes.FindAsync(id);

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            _context.FoodTypes.Update(FoodType);
            await _context.SaveChangesAsync();
            TempData["success"] = "Food Type Updated Successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }

}
