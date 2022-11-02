using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Pool.Client.Authentication;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public  class Login_razor : ComponentBase
{
    //[Inject]  HttpClient httpClient { get; set; }
    [Inject]  AccountService accountService { get; set; }
    [Inject] private IJSRuntime js { get; set; }
    [Inject] private AuthenticationStateProvider authStateProvider { get; set; }
    [Inject] private NavigationManager navManager { get; set; }
    
    protected LoginRequest loginRequest = new LoginRequest();

    protected async Task Authentication()
    {
        try
        {
            var LoginResponse =  await accountService.Login(loginRequest);
            if (LoginResponse != null)
            {
                var userSession = LoginResponse;
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(userSession);
                navManager.NavigateTo("/", true);
            }
            else 
            {
                await js.InvokeVoidAsync("alert", "Invalid User Name or Password");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
       
    }
    
}