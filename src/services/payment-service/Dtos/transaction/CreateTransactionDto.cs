namespace payment_service.Dtos;

public class CreateTransactionDto
{
    public Guid UserId { get; set; }  
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
}