namespace Slice.Web.Pages.Admin.Orders;

[Authorize(Roles = $"{Constants.ManagerRole},{Constants.KitchenRole}")]
public class ManageOrderModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public List<OrderDetailViewModel> OrderDetailViewModels { get; set; } = new();

    public ManageOrderModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task OnGet()
    {
        var orderHeadersFromDb = await _unitOfWork.OrderHeaderRepository.
                GetAllAsync(o => o.Status == Constants.StatusSubmitted ||
                    o.Status == Constants.StatusInProcess);

        foreach (var item in orderHeadersFromDb)
        {
            OrderDetailViewModel individual = new()
            {
                OrderHeader = item,
                OrderDetails = await _unitOfWork.OrderDetailRepository.
                    GetAllAsync(o => o.OrderHeaderId == item.Id)
            };
            OrderDetailViewModels.Add(individual);
        }
    }

    //I will refactor it.
    //I can make a switch or if condition but I think it will be very nesty, I don't know
    public async Task<IActionResult> OnPostOrderInProcess(int orderId)
    {
        await _unitOfWork.OrderHeaderRepository.UpdateStatus(orderId, Constants.StatusInProcess);
        return RedirectToPage("ManageOrder");
    }

    public async Task<IActionResult> OnPostOrderReady(int orderId)
    {
        await _unitOfWork.OrderHeaderRepository.UpdateStatus(orderId, Constants.StatusReady);
        return RedirectToPage("ManageOrder");
    }

    public async Task<IActionResult> OnPostOrderCancel(int orderId)
    {
        await _unitOfWork.OrderHeaderRepository.UpdateStatus(orderId, Constants.StatusCancelled);
        return RedirectToPage("ManageOrder");
    }

}
