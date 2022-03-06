namespace Slice.Models.Entities;
public class FoodType
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
