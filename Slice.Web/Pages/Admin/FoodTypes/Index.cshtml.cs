namespace Slice.Web.Pages.Admin.FoodTypes;
public class IndexModel : PageModel
{
    private readonly IGenericRepository<FoodType> _foodTypeRepository;

    public IReadOnlyList<FoodType> FoodTypes { get; set; }

    public IndexModel(IGenericRepository<FoodType> foodTypeRepository)
            => _foodTypeRepository = foodTypeRepository;

    public async Task OnGet()
        => FoodTypes = await _foodTypeRepository.GetAllAsync();
}
