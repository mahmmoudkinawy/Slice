namespace Slice.Web.Pages.Customer.ShoppingCart;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public double CartTotal { get; set; }
    public IReadOnlyList<Cart> CartList { get; set; }

    public IndexModel(IUnitOfWork unitOfWork )
        => _unitOfWork = unitOfWork;

    public async Task OnGet()
    {
        CartList = await _unitOfWork.CartRepository.GetAllAsync(
           filter: u => u.AppUserId == User.GetUserId(),
           includeProperties: "Product,Product.Category,Product.FoodType");

        foreach (var item in CartList)
            CartTotal += (item.Product.Price * item.Count);
    }

    public async Task<IActionResult> OnPostPlus(int cartId)
    {
        var cartFromDb = await _unitOfWork.CartRepository.GetFirstOrDefaultAsync(c => c.Id == cartId);
        await _unitOfWork.CartRepository.IncrementCount(cartFromDb, 1);
        await _unitOfWork.SaveChangesAsync();
        return RedirectToPage("/Customer/ShoppingCart/Index");
    }

    public async Task<IActionResult> OnPostMinus(int cartId)
    {
        var cartFromDb = await _unitOfWork.CartRepository.GetFirstOrDefaultAsync(c => c.Id == cartId);

        if (cartFromDb.Count == 1)
            await _unitOfWork.CartRepository.Remove(cartFromDb);
        else
            await _unitOfWork.CartRepository.DecrementCount(cartFromDb, 1);

        await _unitOfWork.SaveChangesAsync();
        return RedirectToPage("/Customer/ShoppingCart/Index");
    }

    public async Task<IActionResult> OnPostRemove(int cartId)
    {
        var cartFromDb = await _unitOfWork.CartRepository.GetFirstOrDefaultAsync(c => c.Id == cartId);

        await _unitOfWork.CartRepository.Remove(cartFromDb);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToPage("/Customer/ShoppingCart/Index");
    }

}
