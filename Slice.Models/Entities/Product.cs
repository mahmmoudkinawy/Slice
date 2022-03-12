namespace Slice.Models.Entities;
public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Range(1, 1000, ErrorMessage = "Price should be between $1 and 1000$")]
    public double Price { get; set; }

    [Display(Name = "Image")]
    public string PublicId { get; set; }

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Required]
    [Display(Name = "Food Type")]
    public int FoodTypeId { get; set; }
    [ForeignKey("FoodTypeId")]
    public FoodType FoodType { get; set; }
}