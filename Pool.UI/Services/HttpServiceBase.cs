using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Pool.Shared.Models;
using Pool.UI.Authentication;

namespace Pool.UI.Services;

public abstract class HttpServiceBase
{
    protected HttpClient _client { get; }

    ///private IConfiguration _configuration { get; set; }
    private const string WebApiUrl = "http://localhost:1111/api/";

    private readonly ILocalStorageService _localStorageService;
    private AuthenticationStateProvider _authenticationStateProvider { get; set; }
    protected abstract string _apiControllerName { get; set; }


    protected HttpServiceBase(
        //IConfiguration configuration,
        ILocalStorageService localStorageService)
    {
        // _configuration = configuration;
        _localStorageService = localStorageService;

        _client = new HttpClient();
        _client.BaseAddress = new Uri(WebApiUrl);
        _client.DefaultRequestHeaders.Accept.Clear();
        //_client.Timeout = TimeSpan.FromMinutes(5);
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }


    protected string Url()
    {
        return $"{_apiControllerName}";
    }

    protected string Url(string action)
    {
        return $"{_apiControllerName}/{action}";
    }

    public static async Task<T> DeserializeFromStream<T>(HttpContent content)
    {
        var value = await content.ReadAsStringAsync();
        if (value.Contains("<!DOCTYPE html>"))
            return default;
        return JsonConvert.DeserializeObject<T>(value);
    }

    protected async Task AddAuthorizationAsync(bool skipAuthStateProviderVerification = false)
    {
        string token = string.Empty;

        if (skipAuthStateProviderVerification)
            token = await GetTokenFromLocalstorage();
        else
        {
            try
            {
                token = await ((CustomAuthenticationStateProvider) _authenticationStateProvider).GetToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        if (string.IsNullOrEmpty(token))
            return;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private async Task<string> GetTokenFromLocalstorage()
    {
        var jwt = await _localStorageService.GetItemAsync<object>("UserSessionJWT");

        if (jwt == null)
            return string.Empty;

        var deserializedToken = JsonConvert.DeserializeObject<UserSession>(jwt.ToString());

        if (deserializedToken == null)
            return string.Empty;

        return deserializedToken.Token;
    }
}