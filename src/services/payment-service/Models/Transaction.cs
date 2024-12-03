namespace payment_service.Models;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
