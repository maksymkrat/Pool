using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Pool.Shared.Models;
using Pool.Shared.Models.DeserializeTranslation;

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
    public  List<Word> GetFourRandomWords(Guid userId)
    {
         AddAuthorizationAsync();
        var result =  _client.GetAsync(Url($"GetFourRandomWords/{userId}")).Result;
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<Word>();
        return  DeserializeFromStream<List<Word>>(result.Content).Result;
    }
   
    public async Task<bool> DeleteById(int id)
    {
        await AddAuthorizationAsync();
        var result = await _client.GetAsync(Url($"Delete/{id}"));
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> AddWord(Word word)
    {
        await AddAuthorizationAsync();
        var result = await _client.PostAsync(Url("AddWord"),
            new StringContent(JsonConvert.SerializeObject(word), Encoding.UTF8, "application/json"));
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateWord(Word word)
    {
        await AddAuthorizationAsync();
        var result = await _client.PostAsync(Url("Update"),
            new StringContent(JsonConvert.SerializeObject(word), Encoding.UTF8, "application/json"));
        return result.IsSuccessStatusCode;
    }
    public  Word GetRandomWord(Guid userId)
    {
         AddAuthorizationAsync();
        var result =  _client.GetAsync(Url($"GetRandomWord/{userId}")).Result;
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new Word();
        return  DeserializeFromStream<Word>(result.Content).Result;
    }

    public async Task<Translater> Translate(string word)
    {
        try
        {
            await AddAuthorizationAsync();
            var result = await _client.GetAsync(Url($"Translate/{word}"));
            if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
                return new Translater();
            return await DeserializeFromStream<Translater>(result.Content);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
} 