namespace Slice.Web.Pages.Admin.Categories;
public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IReadOnlyList<Category> Categories { get; set; }

    public IndexModel(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

    public async Task OnGet()
        => Categories = await _unitOfWork.CategoryRepository.GetAllAsync();
}
