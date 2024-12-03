public class TransactionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public DateTime CreatedAt { get; set; }
}