namespace Slice.Web.Pages.Admin.Products;
public class UpsertModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotoService _photoService;

    [BindProperty]
    public Product Product { get; set; }
    public IEnumerable<SelectListItem> FoodTypes { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }

    public UpsertModel(
        IUnitOfWork unitOfWork,
        IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _photoService = photoService;
    }

    public async Task OnGet(int? id)
    {
        if (id != null)
            Product = await _unitOfWork.ProductRepository.GetFirstOrDefaultAsync(p => p.Id == id);
        else
            Product = new();

        var foodTypesFromDb = await _unitOfWork.FoodTypeRepository.GetAllAsync();
        var categoriesFromDb = await _unitOfWork.CategoryRepository.GetAllAsync();

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
            await _unitOfWork.ProductRepository.Add(Product);
        }
        else
        {
            var productFromDb = await _unitOfWork.ProductRepository.GetFirstOrDefaultAsync(p => p.Id == Product.Id);
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
            await _unitOfWork.ProductRepository.Update(productFromDb);
        }
        return RedirectToPage("Index");
    }

}