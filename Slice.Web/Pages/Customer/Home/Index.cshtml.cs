namespace Slice.Web.Pages.Customer.Home;
public class IndexModel : PageModel
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<Product> _productRepository;

    public IReadOnlyList<Product> Products { get; set; }
    public IReadOnlyList<Category> Categories { get; set; }

    public IndexModel(IGenericRepository<Category> categoryRepository,
        IGenericRepository<Product> productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task OnGet()
    {
        Products = await _productRepository.GetAllAsync(includeProperties: "Category,FoodType");
        Categories = await _categoryRepository.GetAllAsync();
    }
}
