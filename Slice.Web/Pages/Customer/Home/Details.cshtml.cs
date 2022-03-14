namespace Slice.Web.Pages.Customer.Home;
public class DetailsModel : PageModel
{
    private readonly IGenericRepository<Product> _productRepository;

    public Product Product { get; set; }

    public DetailsModel(IGenericRepository<Product> productRepository)
        => _productRepository = productRepository;

    public async Task OnGet([FromQuery] int id)
        => Product = await _productRepository.GetFirstOrDefaultAsync(filter: p => p.Id == id,
                includeProperties: "FoodType,Category");
}
