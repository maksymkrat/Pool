﻿using System.Text;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using Pool.Shared.Models;

namespace Pool.UI.Services;

public class AccountService : HttpServiceBase
{
    protected sealed override string _apiControllerName { get; set; }

    public AccountService(ILocalStorageService localStorageService) : base(localStorageService)
    {
        _apiControllerName = "account";
    }

    public async Task<UserSession> Login(LoginRequest loginRequest)
    {
        if (loginRequest == null)
        {
            //return new UserSession();
            return null;
        }
        
        var result = await _client.PostAsync(Url("Login"),
            new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode || string.IsNullOrEmpty(result.Content.ToString()))
            return null;
        return await DeserializeFromStream<UserSession>(result.Content);
    }

}