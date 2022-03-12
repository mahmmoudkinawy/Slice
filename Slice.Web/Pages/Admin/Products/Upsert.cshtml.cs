namespace Slice.Web.Pages.Admin.Products;
public class UpsertModel : PageModel
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<FoodType> _foodTypeRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IPhotoService _photoService;

    [BindProperty]
    public Product Product { get; set; }
    public IEnumerable<SelectListItem> FoodTypes { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }

    public UpsertModel(
        IGenericRepository<Product> productRepository,
        IGenericRepository<FoodType> foodTypeRepository,
        IGenericRepository<Category> categoryRepository,
        IPhotoService photoService)
    {
        _productRepository = productRepository;
        _foodTypeRepository = foodTypeRepository;
        _categoryRepository = categoryRepository;
        _photoService = photoService;
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

    public async Task<IActionResult> OnPostAsync(IFormFile file)
    {
        if (Product.Id == 0)
        {
            var imageResult = await _photoService.AddPhotoAsync(file);
            Product.PublicId = imageResult.Url.ToString();
            TempData["success"] = "Product Created Successfully";
            await _productRepository.Add(Product);
        }
        else
        {

        }
        return RedirectToPage("Index");
    }

}