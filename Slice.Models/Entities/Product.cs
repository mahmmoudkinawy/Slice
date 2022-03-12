namespace Slice.Models.Entities;
public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Range(1, 1000, ErrorMessage = "Price should be between $1 and 1000$")]
    public double Price { get; set; }

    [Required]
    public int PhotoId { get; set; } //not configured yet
    [ForeignKey("PhotoId")]
    public Photo Photo { get; set; } //not configured yet

    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Required]
    public int FoodTypeId { get; set; }
    [ForeignKey("FoodTypeId")]
    public FoodType FoodType { get; set; }
}