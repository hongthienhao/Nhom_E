namespace KoiDeliveryOrderingSystem.Repositories;
public class EditShippingViewModel
{
    public int OrderId { get; set; }
    public string PickupLocation { get; set; }
    public string DeliveryLocation { get; set; }
    public string ShippingMethod { get; set; }
    public double? TotalWeight { get; set; }
    public int? TotalQuantity { get; set; }
}
