namespace Slice.Web.Pages.Customer.ShoppingCart;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICartRepository _cartRepository;
    public IReadOnlyList<Cart> CartList { get; set; }

    public IndexModel(ICartRepository cartRepository)
        => _cartRepository = cartRepository;

    public async Task OnGet()
    //=> CartList = await _cartRepository.GetAllAsync(
    //    filter: u => u.AppUserId == User.GetUserId());
    {
        CartList = await _cartRepository.GetAllAsync(
           filter: u => u.AppUserId == User.GetUserId(),
           includeProperties: "Product,Product.Category,Product.FoodType");
    }

}
