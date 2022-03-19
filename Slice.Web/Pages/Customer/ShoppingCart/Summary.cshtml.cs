namespace Slice.Web.Pages.Customer.ShoppingCart;

[Authorize]
public class SummaryModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IReadOnlyList<Cart> CartList { get; set; }

    [BindProperty]
    public OrderHeader OrderHeader { get; set; }

    //I know that userRepository is a bad practise I will refactor it later
    public SummaryModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        OrderHeader = new();
    }

    public async Task OnGetAsync()
    {
        CartList = await _unitOfWork.CartRepository.GetAllAsync(
           filter: u => u.AppUserId == User.GetUserId(),
           includeProperties: "Product,Product.Category,Product.FoodType");

        foreach (var item in CartList)
            OrderHeader.OrderTotal += (item.Product.Price * item.Count);

        var loggedInUser = await _unitOfWork.AppUserRepository.
                            GetFirstOrDefaultAsync(u => u.Id == User.GetUserId());

        OrderHeader.PickUpName = $"{loggedInUser.FirstName} {loggedInUser.LastName}";
        OrderHeader.PhoneNumber = loggedInUser.PhoneNumber;
    }

    public async Task OnPostAsync()
    {
        CartList = await _unitOfWork.CartRepository.GetAllAsync(
           filter: u => u.AppUserId == User.GetUserId(),
           includeProperties: "Product,Product.Category,Product.FoodType");

        foreach (var item in CartList)
            OrderHeader.OrderTotal += (item.Product.Price * item.Count);

        OrderHeader.AppUserId = User.GetUserId();
        OrderHeader.Status = Constants.StatusPending;
        OrderHeader.OrderDate = DateTime.UtcNow;
        OrderHeader.PickUpTime = DateTime.UtcNow.AddDays(3); //Fake implementation
        //OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " +
        //   OrderHeader.PickUpTime.ToShortTimeString()); //Error because of PostgreSQL

        await _unitOfWork.OrderHeaderRepository.Add(OrderHeader);

        foreach (var item in CartList)
        {
            var orderDetails = new OrderDetail()
            {
                OrderHeaderId = OrderHeader.Id,
                ProductId = item.ProductId,
                Name = item.Product.Name,
                Price = item.Product.Price,
                Count = item.Count
            };
            await _unitOfWork.OrderDetailRepository.Add(orderDetails);
        }

        await _unitOfWork.CartRepository.RemoveRange(CartList);
    }


}