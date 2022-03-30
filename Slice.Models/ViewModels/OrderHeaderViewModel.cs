namespace Slice.Models.ViewModels;
public class OrderHeaderViewModel
{
    public OrderHeader OrderHeader { get; set; }

    public IReadOnlyList<OrderDetail> OrderDetails { get; set; }
}
