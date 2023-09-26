using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Shared.Models;

namespace Pool.Client.Services;

public class IrregularVerbService : HttpServiceBase
{
    public IrregularVerbService(
        AuthenticationStateProvider authenticationStateProvider, 
        ILocalStorageService localStorageService) : base(authenticationStateProvider, localStorageService)
    {
        _apiControllerName = "IrregularVerb";
    }

    protected override string _apiControllerName { get; set; }
    
    public  IrregularVerbModel GetRandomIrregularVerb(UserSessionModel user)
    {
        //AddAuthorizationAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        var result =  _client.GetAsync(Url($"GetRandomIrregularVerb")).Result;
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return new IrregularVerbModel();
        return  DeserializeFromStream<IrregularVerbModel>(result.Content).Result;
    }
}