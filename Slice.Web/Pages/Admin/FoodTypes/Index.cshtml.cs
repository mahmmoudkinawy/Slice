namespace Slice.Web.Pages.Admin.FoodTypes;
public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IReadOnlyList<FoodType> FoodTypes { get; set; }

    public IndexModel(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task OnGet()
        => FoodTypes = await _unitOfWork.FoodTypeRepository.GetAllAsync();
}
