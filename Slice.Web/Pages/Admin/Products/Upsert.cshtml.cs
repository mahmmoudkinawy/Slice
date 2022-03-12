namespace Slice.Web.Pages.Admin.Products;
public class UpsertModel : PageModel
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<FoodType> _foodTypeRepository;
    private readonly IGenericRepository<Category> _categoryRepository;

    public Product Product { get; set; }
    public IEnumerable<SelectListItem> FoodTypes { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }

    public UpsertModel(
        IGenericRepository<Product> productRepository,
        IGenericRepository<FoodType> foodTypeRepository,
        IGenericRepository<Category> categoryRepository)
    {
        _productRepository = productRepository;
        _foodTypeRepository = foodTypeRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task OnGet()
    {
        var foodTypesFromDb = await _foodTypeRepository.GetAllAsync();
        var categoriesFromDb = await _categoryRepository.GetAllAsync();

        Product = new();

        FoodTypes = foodTypesFromDb.Select(f => new SelectListItem
        {
            Text = f.Name,
            Value = f.Id.ToString()
        });

        Categories = categoriesFromDb.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });
    }

}