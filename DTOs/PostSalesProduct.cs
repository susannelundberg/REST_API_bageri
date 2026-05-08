namespace BageriApi.DTOs;

public class PostSalesProduct
{
    public string SalesProductName { get; set; }
    public double PricePerPiece { get; set; }
    public int ProductWeight { get; set; }
    public int QuantityInPackage { get; set; }
}