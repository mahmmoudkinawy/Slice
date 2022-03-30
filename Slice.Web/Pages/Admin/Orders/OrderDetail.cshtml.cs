namespace Slice.Web.Pages.Admin.Orders;

[Authorize]
public class OrderDetailModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderHeaderViewModel OrderHeaderViewModel { get; set; }

    public OrderDetailModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task OnGet([FromQuery] int id)
    {
        OrderHeaderViewModel = new()
        {
            OrderHeader = await _unitOfWork.OrderHeaderRepository.GetFirstOrDefaultAsync(o => o.Id == id,
                            includeProperties: "AppUser"),
            OrderDetails = await _unitOfWork.OrderDetailRepository.
                            GetAllAsync(o => o.OrderHeaderId == id)
        };
    }
}
