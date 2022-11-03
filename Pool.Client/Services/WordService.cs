using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Shared.Models;

namespace Pool.Client.Services;

public class WordService : HttpServiceBase
{
    public WordService(
        AuthenticationStateProvider authenticationStateProvider, 
        ILocalStorageService localStorageService) : base(authenticationStateProvider, localStorageService)
    {
        _apiControllerName = "word";
    }

    protected override string _apiControllerName { get; set; }

    public async Task<List<Word>> GetAllWords()
    {
         await AddAuthorizationAsync();
        var result = await _client.GetAsync(Url("GetAllUsersWords"));
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<Word>();
        return await DeserializeFromStream<List<Word>>(result.Content);
            
    }
} 