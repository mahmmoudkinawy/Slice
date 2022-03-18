namespace Slice.Web.Pages.Customer.Home;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly ICartRepository _cartRepository;
    private readonly IGenericRepository<Product> _productRepository;

    [BindProperty]
    public Cart Cart { get; set; }

    public DetailsModel(ICartRepository cartRepository,
        IGenericRepository<Product> productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task OnGetAsync([FromQuery] int id)
    {
        Cart = new()
        {
            ProductId = id,
            Product = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == id,
                includeProperties: "Category,FoodType"),
            AppUserId = User.GetUserId()
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _cartRepository.Add(Cart);
            TempData["success"] = "Added Products Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
