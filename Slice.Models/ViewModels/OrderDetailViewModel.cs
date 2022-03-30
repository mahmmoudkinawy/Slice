namespace Slice.Models.ViewModels;
public class OrderDetailViewModel
{
    public OrderHeader OrderHeader { get; set; }

    public IReadOnlyList<OrderDetail> OrderDetails { get; set; }
}
