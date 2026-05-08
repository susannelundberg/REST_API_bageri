namespace BageriApi.Entities;

public class Customer
{
    public int Id { get; set; }
    public string StoreName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string ContactPerson { get; set; }
    public string DeliveryAddress { get; set; }
    public string InvocieAddress { get; set; }
    public List<Order> OrderHistory { get; set; } = new();
}
