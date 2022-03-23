namespace Slice.Web.Pages.Customer.Home;
public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IReadOnlyList<Models.Entities.Product> Products { get; set; }
    public IReadOnlyList<Category> Categories { get; set; }

    public IndexModel(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task OnGet()
    {
        Products = await _unitOfWork.ProductRepository.GetAllAsync(includeProperties: "Category,FoodType");
        Categories = await _unitOfWork.CategoryRepository.GetAllAsync(orderBy: c => c.OrderBy(c => c.DisplayOrder));
    }
}
