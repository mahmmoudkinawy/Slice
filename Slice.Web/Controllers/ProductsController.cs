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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var productFromDb = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == id);
        await _productRepository.Remove(productFromDb);
        return Json(new { success = true, message = "Deleted Successfully" });
    }

}