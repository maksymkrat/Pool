using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Shared.Models;

namespace Pool.Client.Services;

public class IrregularVerbService : HttpServiceBase
{
    public IrregularVerbService(
        IConfiguration configuration,
        AuthenticationStateProvider authenticationStateProvider, 
        ILocalStorageService localStorageService) : base(configuration,authenticationStateProvider, localStorageService)
    {
        _apiControllerName = "IrregularVerb";
    }

    protected override string _apiControllerName { get; set; }
    
    public async  Task<IrregularVerbModel> GetRandomIrregularVerb(UserSessionModel user)
    {
        await AddAuthorizationAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        var result =  _client.GetAsync(Url($"GetRandomIrregularVerb")).Result;
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new IrregularVerbModel();
        return  DeserializeFromStream<IrregularVerbModel>(result.Content).Result;
    }
}