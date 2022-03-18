namespace Slice.Web.Pages.Customer.Home;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IGenericRepository<Product> _productRepository;

    [BindProperty]
    public Cart Cart { get; set; }

    public DetailsModel(IGenericRepository<Product> productRepository)
        => _productRepository = productRepository;

    public async Task OnGetAsync([FromQuery] int id)
    {
        Cart = new()
        {
            Product = await _productRepository.GetFirstOrDefaultAsync(filter: p => p.Id == id,
                includeProperties: "FoodType,Category")
        };
    }

    public async Task OnPostAsync()
    {
        
    }
}
