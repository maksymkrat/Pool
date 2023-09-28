using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Pool.Client.Authentication;
using Pool.Shared.Models;

namespace Pool.Client.Services;

public class AccountService : HttpServiceBase
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly SessionService _sessionService;
    protected sealed override string _apiControllerName { get; set; }
   

    public AccountService(
        IConfiguration configuration,
        AuthenticationStateProvider authenticationStateProvider, 
            ILocalStorageService localStorageService, SessionService sessionService) : base(configuration,authenticationStateProvider, localStorageService)
    {
        _apiControllerName = "account";
        _authStateProvider = authenticationStateProvider;
        _sessionService = sessionService;
    }

    public async Task<UserSessionModel> Login(LoginRequestModel loginRequest)
    {
        if (loginRequest == null)
        {
            //return new UserSession();
            return null;
        }
        
        var result = await _client.PostAsync(Url("Login"),
            new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return null;
        return _sessionService.UserSession = await DeserializeFromStream<UserSessionModel>(result.Content);
        
    }

    public async Task<bool> Create(RegistrationModel newUser)
    {
        var result = await _client.PostAsync(Url("Create"),
            new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));
        return result.IsSuccessStatusCode;
    }


}