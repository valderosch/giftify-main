namespace payment_service.Dtos;

public class UpdateSubscriptionDto
{
    public string PlanId { get; set; }  
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; } 
}