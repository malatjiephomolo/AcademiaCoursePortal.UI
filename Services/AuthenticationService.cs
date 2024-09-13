
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace AcademiaCoursePortal.UI.Services;
public class AuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "https://localhost:7191/api/authentication";

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var loginModel = new LoginModel { Username = username, Password = password };
        var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/login", loginModel);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token;
        }
        return null;
    }

    public async Task RegisterAsync(string name, string username, string password, string email)
    {
        var registerModel = new RegisterModel { Name = name, Username = username, Password = password, Email = email };
        await _httpClient.PostAsJsonAsync($"{_apiUrl}/register", registerModel);
    }
}

public class LoginModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class RegisterModel
{
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
}

public class LoginResponse
{
    public string? Token { get; set; }
}
