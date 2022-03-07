namespace Slice.Web.Pages.Admin.Categories;
public class IndexModel : PageModel
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public IReadOnlyList<Category> Categories { get; set; }

    public IndexModel(IGenericRepository<Category> categoryRepository)
        => _categoryRepository = categoryRepository;

    public async Task OnGet()
        => Categories = await _categoryRepository.GetAllAsync();
}
