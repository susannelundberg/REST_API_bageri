namespace BageriApi.DTO;

public class PostSupplierProductModel
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public decimal PricePerKg { get; set; }
    public string Name { get; set; }
    public string ArticleNumber { get; set; }

}
