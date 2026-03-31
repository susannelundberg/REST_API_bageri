using BageriApi.Entities;

namespace BageriApi;

public class SupplierProduct
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public decimal PricePerKg { get; set; }
    public Product Product { get; set; }
    public Supplier Supplier { get; set; }
}
