namespace payment_service.Dtos;

public class TopUpBalanceDto
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
}