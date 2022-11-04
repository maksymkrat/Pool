using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
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

    public async Task<List<Word>> GetAllWords(Guid userId)
    {
         await AddAuthorizationAsync();
        var result = await _client.GetAsync(Url($"GetAllUsersWords/{userId}"));
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<Word>();
        return await DeserializeFromStream<List<Word>>(result.Content);
            
    }
} 