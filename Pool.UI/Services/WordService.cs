using Blazored.LocalStorage;
using Pool.Shared.Models;

namespace Pool.UI.Services;

public class WordService : HttpServiceBase
{
    public WordService(ILocalStorageService localStorageService) : base(localStorageService)
    {
        _apiControllerName = "word";
    }

    protected override string _apiControllerName { get; set; }

    public async Task<List<Word>> GetAllWords()
    {

        var result = await _client.GetAsync(Url("GetAllUsersWords"));
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<Word>();
        return await DeserializeFromStream<List<Word>>(result.Content);
            
    }
}