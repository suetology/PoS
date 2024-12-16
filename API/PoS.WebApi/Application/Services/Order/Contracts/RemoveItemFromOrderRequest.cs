public class RemoveItemFromOrderRequest
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public Guid BusinessId { get; set; }
}
