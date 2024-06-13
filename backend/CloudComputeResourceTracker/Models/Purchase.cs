public class Purchase
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public DateTime PurchasedAt { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public string? Description { get; set; }
}
