namespace Slice.Web.Pages.Customer.ShoppingCart;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICartRepository _cartRepository;
    public double CartTotal { get; set; }
    public IReadOnlyList<Cart> CartList { get; set; }

    public IndexModel(ICartRepository cartRepository)
        => _cartRepository = cartRepository;

    public async Task OnGet()
    {
        CartList = await _cartRepository.GetAllAsync(
           filter: u => u.AppUserId == User.GetUserId(),
           includeProperties: "Product,Product.Category,Product.FoodType");

        foreach (var item in CartList)
            CartTotal += (item.Product.Price * item.Count);
    }

}
