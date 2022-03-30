namespace Slice.Web.Pages.Admin.Orders;

[Authorize]
public class OrderDetailModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderDetailViewModel OrderHeaderViewModel { get; set; }

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

    public async Task<IActionResult> OnPostOrderCompleted(int orderId)
    {
        await _unitOfWork.OrderHeaderRepository.UpdateStatus(orderId, Constants.StatusCompleted);
        return RedirectToPage("OrderList");
    }

    public async Task<IActionResult> OnPostOrderCancel(int orderId)
    {
        await _unitOfWork.OrderHeaderRepository.UpdateStatus(orderId, Constants.StatusCancelled);
        return RedirectToPage("OrderList");
    }

    public async Task<IActionResult> OnPostOrderRefund(int orderId)
    {
        var orderHeaderFromDb = await _unitOfWork.OrderHeaderRepository.
                GetFirstOrDefaultAsync(o => o.Id == orderId);

        var options = new RefundCreateOptions
        {
            Reason = RefundReasons.RequestedByCustomer,
            PaymentIntent = orderHeaderFromDb.PaymentIntentId
        };

        var service = new RefundService();
        var refund = await service.CreateAsync(options);

        await _unitOfWork.OrderHeaderRepository.UpdateStatus(orderId, Constants.StatusRefunded);
        return RedirectToPage("OrderList");
    }
}
