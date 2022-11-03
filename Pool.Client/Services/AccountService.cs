using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Pool.Shared.Models;

namespace Pool.Client.Services;

public class AccountService : HttpServiceBase
{
    protected sealed override string _apiControllerName { get; set; }
    public UserSession UserSession { get; set; }

    public AccountService(
            AuthenticationStateProvider authenticationStateProvider, 
            ILocalStorageService localStorageService) : base(authenticationStateProvider, localStorageService)
    {
        _apiControllerName = "account";
    }

    public async Task<UserSession> Login(LoginRequest loginRequest)
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
        UserSession = await DeserializeFromStream<UserSession>(result.Content);
        return UserSession;
    }

}