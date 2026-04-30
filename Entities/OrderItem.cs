namespace BageriApi;

public class OrderItem
{
    public int Id { get; set; }
    public SalesProduct Product { get; set; }
    public int Quantity { get; set; }
    public double SubTotal { get; set; }
}
