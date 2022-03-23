namespace Slice.Web.Pages.Customer.ShoppingCart;
public class OrderConfirmationModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public int OrderId { get; set; }

    public OrderConfirmationModel(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task OnGetAsync([FromQuery] int id)
    {
        var orderHeaderFromDb = await _unitOfWork.OrderHeaderRepository.
                GetFirstOrDefaultAsync(o => o.Id == id);
        if (orderHeaderFromDb.SessionId != null)
        {
            var service = new SessionService();
            var session = await service.GetAsync(orderHeaderFromDb.SessionId);
            if (session.PaymentStatus.ToLower() == "paid")
            {
                orderHeaderFromDb.Status = Constants.StatusSubmitted;
                await _unitOfWork.SaveChangesAsync();
            }
        }

        var cartFromDb = await _unitOfWork.CartRepository.
                GetAllAsync(u => u.AppUserId == orderHeaderFromDb.AppUserId);
        await _unitOfWork.CartRepository.RemoveRange(cartFromDb);
        await _unitOfWork.SaveChangesAsync();
        OrderId = id;
    }
}
