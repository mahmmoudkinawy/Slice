namespace Slice.Web.Pages.Admin.Products;
public class UpsertModel : PageModel
{
    //I know the constructor must not inject to much Interfaces as I did 
    //I will try to refactor it and I know I can do it using unit of work, but I'm trying to avoid this
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

    public async Task OnGet(int? id)
    {
        if (id != null)
            Product = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == id);
        else
            Product = new();

        var foodTypesFromDb = await _foodTypeRepository.GetAllAsync();
        var categoriesFromDb = await _categoryRepository.GetAllAsync();

        FoodTypes = foodTypesFromDb.ToSelectListItem();
        Categories = categoriesFromDb.ToSelectListItem();
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
            var productFromDb = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == Product.Id);
            if (file?.Length > 0)
            {
                var imageResult = await _photoService.AddPhotoAsync(file);
                productFromDb.PublicId = imageResult.Url.ToString();
            }

            //Can not bind Product because it has been tracking by another 
            productFromDb.Name = Product.Name;
            productFromDb.Description = Product.Description;
            productFromDb.FoodTypeId = Product.FoodTypeId;
            productFromDb.CategoryId = Product.CategoryId;

            TempData["success"] = "Product Updated Successfully";
            await _productRepository.Update(productFromDb);
        }
        return RedirectToPage("Index");
    }

}