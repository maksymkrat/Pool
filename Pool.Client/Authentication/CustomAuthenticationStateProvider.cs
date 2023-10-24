using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ILogger _logger;
    private readonly SessionService _sessionService;


    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, ILogger<CustomAuthenticationStateProvider> logger, SessionService sessionService)
    {
        _localStorageService = localStorageService;
        _logger = logger;
        _sessionService = sessionService;
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetAuthenticationStateAsync");

        try
        {
            var userSession = await _localStorageService.ReadEncryptedItemAsync<UserSessionModel>("UserSessionJWT");
            if (userSession == null  || userSession.ExpiryTimeStamp <= DateTime.Now)
                return await Task.FromResult(new AuthenticationState(_anonymous));
            else
            {
                _sessionService.UserSession = userSession;
            }
            
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role)
            }, "JwtAuth"));
           

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch (Exception)
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(UserSessionModel? userSession)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: UpdateAuthenticationState");

        ClaimsPrincipal claimsPrincipal;
        if (userSession != null)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role)
            }));

            userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiryIn);
            await _localStorageService.SaveItemEncryptedAsync("UserSessionJWT", userSession);
        }
        else
        {
            claimsPrincipal = _anonymous;
            await _localStorageService.RemoveItemAsync("UserSessionJWT");
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task<String> GetToken()
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetToken");

        var result = string.Empty;
        try
        {
            var userSession = await _localStorageService.ReadEncryptedItemAsync<UserSessionModel>("UserSessionJWT");
           
            if (userSession != null)
            {
                DateTime timeNow = DateTime.Now;
                userSession.ExpiryTimeStamp = timeNow.AddSeconds(userSession.ExpiryIn);
                await _localStorageService.SaveItemEncryptedAsync("UserSessionJWT", userSession);
                
                result = userSession.Token;
                
            }
                
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }
}