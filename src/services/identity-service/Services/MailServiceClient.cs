using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using identity_service.Dtos;

public class MailServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _mailServiceUrl;

    public MailServiceClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _mailServiceUrl = configuration["MailService:Url"];
    }

    public async Task<bool> SendPasswordResetEmailAsync(PasswordResetRequestDto request)
    {
        var jsonContent = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_mailServiceUrl, content);

        return response.IsSuccessStatusCode;
    }
}
