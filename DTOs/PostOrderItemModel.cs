using BageriApi.Entities;

namespace BageriApi.DTOs;

public class PostOrderItemModel
{
    public int SalesProductId { get; set; }
    public double Quantity { get; set; }
    public SalesProduct SalesProduct { get; set; }

}