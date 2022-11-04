using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
      private readonly ILocalStorageService _localStorageService;

        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _localStorageService.ReadEncryptedItemAsync<UserSession>("UserSessionJWT");
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));
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

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
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
            var result = string.Empty;
            try
            {
                var userSession = await _localStorageService.ReadEncryptedItemAsync<UserSession>("UserSessionJWT");
                if (userSession != null)
                    result = userSession.Token;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                
            }
            return result;
        }
}