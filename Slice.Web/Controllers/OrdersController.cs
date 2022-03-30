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
    public async Task<IActionResult> GetOrders(string? status = null)
    {
        var orderHeadersFromDb = await _unitOfWork.OrderHeaderRepository.
                        GetAllAsync(includeProperties: "AppUser");

        if (status == "cancelled")
            orderHeadersFromDb.Where(o => o.Status == Constants.StatusCancelled);
        else if (status == "inProcess")
            orderHeadersFromDb.Where(o => o.Status == Constants.StatusInProcess);
        else if (status == "completed")
            orderHeadersFromDb.Where(o => o.Status == Constants.StatusCompleted);
        else
            orderHeadersFromDb.Where(o => o.Status == Constants.StatusReady);

        //orderHeadersFromDb = status switch
        //{
        //    "cancelled" => orderHeadersFromDb.Where(o => o.Status == Constants.StatusCancelled),
        //    "inProcess" => orderHeadersFromDb.Where(o => o.Status == Constants.StatusInProcess),
        //    "completed" => orderHeadersFromDb.Where(o => o.Status == Constants.StatusCompleted),
        //    "ready" => orderHeadersFromDb.Where(o => o.Status == Constants.StatusReady),
        //    _ => orderHeadersFromDb
        //};

        return Json(new
        {
            data = orderHeadersFromDb
        });
    }
}