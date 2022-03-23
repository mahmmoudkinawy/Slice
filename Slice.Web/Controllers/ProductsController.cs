namespace Slice.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetProducts()
        => Json(new { data = await _unitOfWork.ProductRepository.GetAllAsync(includeProperties: "Category,FoodType") });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var productFromDb = await _unitOfWork.ProductRepository.GetFirstOrDefaultAsync(p => p.Id == id);
        await _unitOfWork.ProductRepository.Remove(productFromDb);
        return Json(new { success = true, message = "Deleted Successfully" });
    }

}