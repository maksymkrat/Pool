using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Pool.Client.Authentication;
using Pool.Client.Components;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Login_razor : ComponentBase
{
    [Inject] private AccountService _accountService { get; set; }
    [Inject] private IJSRuntime _js { get; set; }
    [Inject] private AuthenticationStateProvider _authStateProvider { get; set; }
    [Inject] private NavigationManager _navManager { get; set; }
    [Inject]  NavigationManager navManager { get; set; }
    protected NotificationType NotificationType { get; set; }
    protected string NotificationText { get; set; }
    protected Notification Notification { get; set; }
    protected bool DisplayNotification { get; set; }

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
                NotificationType = NotificationType.Danger;
                NotificationText = "Invalid Email or Password";
                DisplayNotification = true;
                InvokeAsync(StateHasChanged);
                await Task.Delay(3000);
                DisplayNotification = false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    protected void RestorePassword()
    {
        navManager.NavigateTo("/Restore", true);
    }
}