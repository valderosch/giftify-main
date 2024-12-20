using System.Text;
using System.Text.Json;
using AutoMapper;
using payment_service.Dtos;
using payment_service.Dtos.mail;
using payment_service.Interfaces;
using payment_service.Models;

namespace payment_service.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IMapper mapper,
        HttpClient httpClient,
        ILogger<TransactionService> logger)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, string ErrorMessage, TransactionDto Transaction)> CreateTransactionAsync(
        CreateTransactionDto dto)
    {
        if (dto.Amount <= 0)
            return (false, "Amount must be greater than zero.", null);

        var transaction = _mapper.Map<Transaction>(dto);
        transaction.CreatedAt = DateTime.UtcNow;

        await _transactionRepository.AddAsync(transaction);
        
        _ = SendEmailAsync(dto.UserId, transaction);

        return (true, null, _mapper.Map<TransactionDto>(transaction));
    }

    public async Task<TransactionDto> GetTransactionByIdAsync(Guid id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        return transactions.Select(t => _mapper.Map<TransactionDto>(t));
    }

    public async Task<bool> DeleteTransactionAsync(Guid id)
    {
        return await _transactionRepository.DeleteAsync(id);
    }

    private async Task SendEmailAsync(Guid userId, Transaction transaction)
    {
        try
        {
            var userResponse =
                await _httpClient.GetAsync(
                    $"http://localhost:5000/identity/user/GetUserProfile/profile?userId={userId}");
            if (!userResponse.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Failed to retrieve user data for UserId: {userId}");
                return;
            }

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserProfileDto>(userContent);

            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                _logger.LogWarning($"Invalid user data retrieved for UserId: {userId}");
                return;
            }

            var emailBody =
                $"Transaction Details:\nAmount: {transaction.Amount}\nDate: {transaction.CreatedAt}\nTransaction Type: {transaction.TransactionType}";
            var emailRequest = new
            {
                email = user.Email,
                subject = "Transaction Notification",
                body = emailBody
            };

            var content = new StringContent(JsonSerializer.Serialize(emailRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5000/mailing/PasswordReset/send", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to send email to {user.Email} for TransactionId: {transaction.Id}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred while sending email for TransactionId: {transaction.Id}");
        }
    }
}

