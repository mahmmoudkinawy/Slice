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

    public async Task<IActionResult> OnPostPlus(int cartId)
    {
        var cartFromDb = await _cartRepository.GetFirstOrDefaultAsync(c => c.Id == cartId);
        await _cartRepository.IncrementCount(cartFromDb, 1);
        return RedirectToPage("/Customer/ShoppingCart/Index");
    }

    public async Task<IActionResult> OnPostMinus(int cartId)
    {
        var cartFromDb = await _cartRepository.GetFirstOrDefaultAsync(c => c.Id == cartId);

        if (cartFromDb.Count == 1)
            await _cartRepository.Remove(cartFromDb);
        else
            await _cartRepository.DecrementCount(cartFromDb, 1);

        return RedirectToPage("/Customer/ShoppingCart/Index");
    }

    public async Task<IActionResult> OnPostRemove(int cartId)
    {
        var cartFromDb = await _cartRepository.GetFirstOrDefaultAsync(c => c.Id == cartId);

        await _cartRepository.Remove(cartFromDb);

        return RedirectToPage("/Customer/ShoppingCart/Index");
    }

}
