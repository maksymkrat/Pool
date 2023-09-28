using System.Net.Http.Headers;
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
        IConfiguration configuration,
        AuthenticationStateProvider authenticationStateProvider, 
        ILocalStorageService localStorageService) : base(configuration,authenticationStateProvider, localStorageService)
    {
        _apiControllerName = "word";
    }

    protected override string _apiControllerName { get; set; }

    public  List<WordModel> GetAllWords(UserSessionModel user)
    {
          //AddAuthorizationAsync();
          _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        var result =  _client.GetAsync(Url($"GetAllUsersWords/{user.Id}")).Result;
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<WordModel>();
        return  DeserializeFromStream<List<WordModel>>(result.Content).Result;
    }
    public async  Task<List<WordModel>> SearchTranslatedWords(string word, Guid id)
    {
        await AddAuthorizationAsync();
        var result =  await _client.GetAsync(Url($"SearchTranslatedWords/{word}/{id.ToString()}"));
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<WordModel>();
        return  await DeserializeFromStream<List<WordModel>>(result.Content);
    }
    public async  Task<List<WordModel>> SearchWords(string word, Guid id)
    {
        await AddAuthorizationAsync();
        var result =  await _client.GetAsync(Url($"SearchWords/{word}/{id.ToString()}"));
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<WordModel>();
        return  await DeserializeFromStream<List<WordModel>>(result.Content);
    }
    
    
    public  List<WordModel> GetFourRandomWords(UserSessionModel user)
    {
         //AddAuthorizationAsync();
         _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        var result =  _client.GetAsync(Url($"GetFourRandomWords/{user.Id}")).Result;
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new List<WordModel>();
        return  DeserializeFromStream<List<WordModel>>(result.Content).Result;
    }
   
    public async  Task<bool> DeleteById(int id)
    {
        await AddAuthorizationAsync();
        var result = await  _client.GetAsync(Url($"Delete/{id}"));
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> AddWord(WordModel word)
    {
        await AddAuthorizationAsync();
        var result = await _client.PostAsync(Url("AddWord"),
            new StringContent(JsonConvert.SerializeObject(word), Encoding.UTF8, "application/json"));
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateWord(WordModel word)
    {
        await AddAuthorizationAsync();
        var result = await _client.PostAsync(Url("Update"),
            new StringContent(JsonConvert.SerializeObject(word), Encoding.UTF8, "application/json"));
        return result.IsSuccessStatusCode;
    }
    public  WordModel GetRandomWord(UserSessionModel user)
    {
         //AddAuthorizationAsync();
         _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        var result =  _client.GetAsync(Url($"GetRandomWord/{user.Id}")).Result;
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new WordModel();
        return  DeserializeFromStream<WordModel>(result.Content).Result;
    }

    public  Translator Translate(string word)
    {
        try
        {
             AddAuthorizationAsync();
            var result =  _client.GetAsync(Url($"Translate/{word}")).Result;
            if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
                return new Translator();
            return  DeserializeFromStream<Translator>(result.Content).Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
} 