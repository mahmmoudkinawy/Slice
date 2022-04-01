namespace Slice.Web.Pages.Customer.Home;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public Cart Cart { get; set; }

    public DetailsModel(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task OnGetAsync([FromQuery] int id)
    {
        Cart = new()
        {
            ProductId = id,
            Product = await _unitOfWork.ProductRepository.GetFirstOrDefaultAsync(p => p.Id == id,
                includeProperties: "Category,FoodType"),
            AppUserId = User.GetUserId()
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var cartFromDb = await _unitOfWork.CartRepository.GetFirstOrDefaultAsync(
                filter: u => u.AppUserId == User.GetUserId() &&
                u.ProductId == Cart.ProductId);

            if (cartFromDb == null)
            {
                var shoppingCart = await
                    _unitOfWork.CartRepository.GetAllAsync(u => u.AppUserId == User.GetUserId());

                await _unitOfWork.CartRepository.Add(Cart);
                HttpContext.Session.SetInt32(Constants.SessionCart, shoppingCart.Count + 1);
            }
            else
                await _unitOfWork.CartRepository.IncrementCount(cartFromDb, Cart.Count);

            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Added Products Successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
