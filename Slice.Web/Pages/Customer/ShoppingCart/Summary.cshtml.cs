namespace Slice.Web.Pages.Customer.ShoppingCart;

[Authorize]
public class SummaryModel : PageModel
{
    private readonly ICartRepository _cartRepository;
    private readonly IGenericRepository<OrderHeader> _orderHeaderRepository;
    private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
    private readonly IGenericRepository<AppUser> _userRepository;

    public IReadOnlyList<Cart> CartList { get; set; }

    [BindProperty]
    public OrderHeader OrderHeader { get; set; }

    //I know that userRepository is a bad practise I will refactor it later
    public SummaryModel(ICartRepository cartRepository,
        IGenericRepository<OrderHeader> orderHeaderRepository,
        IGenericRepository<OrderDetail> orderDetailRepository,
        IGenericRepository<AppUser> userRepository)
    {
        _cartRepository = cartRepository;
        _orderHeaderRepository = orderHeaderRepository;
        _orderDetailRepository = orderDetailRepository;
        _userRepository = userRepository;
        OrderHeader = new();
    }

    public async Task OnGetAsync()
    {
        CartList = await _cartRepository.GetAllAsync(
           filter: u => u.AppUserId == User.GetUserId(),
           includeProperties: "Product,Product.Category,Product.FoodType");

        foreach (var item in CartList)
            OrderHeader.OrderTotal += (item.Product.Price * item.Count);

        var loggedInUser = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == User.GetUserId());

        OrderHeader.PickUpName = $"{loggedInUser.FirstName} {loggedInUser.LastName}";
        OrderHeader.PhoneNumber = loggedInUser.PhoneNumber;
    }

    public async Task OnPostAsync()
    {
        CartList = await _cartRepository.GetAllAsync(
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

        await _orderHeaderRepository.Add(OrderHeader);

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
            await _orderDetailRepository.Add(orderDetails);
        }

        await _cartRepository.RemoveRange(CartList);
    }


}