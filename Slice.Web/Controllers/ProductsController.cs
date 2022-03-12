namespace Slice.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IGenericRepository<Product> _productRepository;

    public ProductsController(IGenericRepository<Product> productRepository)
        => _productRepository = productRepository;

    [HttpGet]
    public async Task<IActionResult> GetProducts()
        => Json(new { data = await _productRepository.GetAllAsync("Category,FoodType") });
}