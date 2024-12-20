namespace payment_service.Dtos;

public class CreateSubscriptionDto
{
    public Guid UserId { get; set; }
    public string PlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; } = "Active";
}