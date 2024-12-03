namespace payment_service.Dtos;

public class WithdrawBalanceDto
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
}