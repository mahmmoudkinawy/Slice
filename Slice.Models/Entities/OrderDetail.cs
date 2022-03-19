namespace Slice.Models.Entities;
public class OrderDetail
{
    public int Id { get; set; }

    [Required]
    public int OrderHeaderId { get; set; }

    [ForeignKey(nameof(OrderHeaderId))]
    public OrderHeader OrderHeader { get; set; }

    [Required]
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    public int Count { get; set; }

    [Required]
    public double Price { get; set; }
    public string Name { get; set; }
}
