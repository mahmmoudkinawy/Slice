namespace Slice.Web.Pages.Admin.Orders;

[Authorize]
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
}
