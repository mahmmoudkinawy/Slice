namespace Slice.Web.Pages.Customer.ShoppingCart;

[Authorize]
public class SummaryModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IReadOnlyList<Cart> CartList { get; set; }

    [BindProperty]
    public OrderHeader OrderHeader { get; set; }

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

    public async Task<IActionResult> OnPostAsync()
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
        await _unitOfWork.SaveChangesAsync();

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
            await _unitOfWork.SaveChangesAsync();
        }

        await _unitOfWork.SaveChangesAsync();

        var domain = "https://" + HttpContext.Request.Host;
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            SuccessUrl = domain + "/Customer/Home/Index",
            CancelUrl = domain + "/cancel.html"
        };

        foreach (var item in CartList)
        {
            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Product.Price * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.Name
                    }
                },
                Quantity = item.Count
            };
            options.LineItems.Add(sessionLineItem);
        }

        var service = new SessionService();
        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);

    }


}