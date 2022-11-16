using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Pool.Client.Authentication;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Login_razor : ComponentBase
{
    [Inject] private AccountService _accountService { get; set; }
    [Inject] private IJSRuntime _js { get; set; }
    [Inject] private AuthenticationStateProvider _authStateProvider { get; set; }
    [Inject] private NavigationManager _navManager { get; set; }

    protected LoginRequestModel loginRequest = new LoginRequestModel();

    protected async Task Authentication()
    {
        try
        {
            var LoginResponse = await _accountService.Login(loginRequest);
            if (LoginResponse != null)
            {
                var userSession = LoginResponse;
                var customAuthStateProvider = (CustomAuthenticationStateProvider) _authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(userSession);
                _navManager.NavigateTo("/", true);
            }
            else
            {
                await _js.InvokeVoidAsync("alert", "Invalid User Name or Password");  //TODO: Change on blazor
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}