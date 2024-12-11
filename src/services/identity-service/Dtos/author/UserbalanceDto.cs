namespace identity_service.Dtos.author;

public class UserBalanceDto
{
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
}