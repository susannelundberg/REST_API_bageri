namespace BageriApi.DTO;

public class PutSupplierProductModel
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public decimal PricePerKg { get; set; }

}
