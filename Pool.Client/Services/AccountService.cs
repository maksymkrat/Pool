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
    protected sealed override string _apiControllerName { get; set; }
   

    public AccountService(
        AuthenticationStateProvider authenticationStateProvider, 
            ILocalStorageService localStorageService) : base(authenticationStateProvider, localStorageService)
    {
        _apiControllerName = "account";
        _authStateProvider = authenticationStateProvider;
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
        return ((CustomAuthenticationStateProvider)_authStateProvider).UserSession = await DeserializeFromStream<UserSessionModel>(result.Content);
        
    }

    public async Task<bool> Create(RegistrationModel newUser)
    {
        var result = await _client.PostAsync(Url("Create"),
            new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));
        return result.IsSuccessStatusCode;
    }


}