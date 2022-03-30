namespace Slice.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdersController(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOrders()
        => Json(new
        {
            data = await _unitOfWork.OrderHeaderRepository.
                        GetAllAsync(includeProperties: "AppUser")
        });
}