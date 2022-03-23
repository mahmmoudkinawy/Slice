namespace Slice.Models.Entities;
public class Cart
{
    public int Id { get; set; }

    [Range(1, 100, ErrorMessage = "Please select a count between 1 and 100")]
    public int Count { get; set; } = 1;

    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    [ValidateNever]
    public Product Product { get; set; }

    public string AppUserId { get; set; }

    [ForeignKey(nameof(AppUserId))]
    [ValidateNever]
    public AppUser AppUser { get; set; }
}
