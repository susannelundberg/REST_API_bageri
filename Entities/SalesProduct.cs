namespace BageriApi;

public class SalesProduct
{
    public int Id { get; set; }
    public string SalesProductName { get; set; }
    public double PricePerPiece { get; set; }
    public int ProductWeight { get; set; }
    public int QuantityInPackage { get; set; }
    public DateTime BestBeforeDate { get; set; } = DateTime.Now;
    public DateTime ManufacturingDate { get; set; } = DateTime.Now;
}
