namespace BageriApi.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int SalesProductId { get; set; }
    public SalesProduct SalesProduct { get; set; }
    public double Quantity { get; set; }
    public double SubTotal { get; set; }
}
