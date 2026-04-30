namespace BageriApi;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> OrderItem { get; set; } = [];
    public double TotalPrice { get; set; }
}
