namespace Slice.Web.ViewComponents;
public class CartViewComponent : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork;

    public CartViewComponent(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var count = 0;

        if (claim != null)
        {
            if (HttpContext.Session.GetInt32(Constants.SessionCart) != null)
                return View(HttpContext.Session.GetInt32(Constants.SessionCart));
            else
            {
                var cart = await _unitOfWork.CartRepository.GetAllAsync(u => u.AppUserId == claim.Value);
                count = cart.Count;
                HttpContext.Session.SetInt32(Constants.SessionCart, count);
                return View(count);
            }
        }
        else
        {
            HttpContext.Session.Clear();
            return View(count);
        }

    }
}