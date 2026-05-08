namespace BageriApi.DTOs;

public class PostOrderModel
{
    public int CustomerId { get; set; }
    public List<PostOrderItemModel> OrderItems { get; set; } = new();
    

}