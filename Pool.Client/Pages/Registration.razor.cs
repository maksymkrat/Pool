using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Registration_razor : ComponentBase
{
    [Inject] private AccountService _accountService { get; set; }
    [Inject] private NavigationManager _navManager { get; set; }
    [Inject] private IJSRuntime _js { get; set; }

    protected RegistrationModel newUser = new RegistrationModel();

    protected async void HandleValidationSubmit()
    {
        var result = await _accountService.Create(newUser);
        if (result)
        {
             //TODO: crate notification about successful registration
            await Task.Delay(1500);
            _navManager.NavigateTo("/Login",true);

        }
        
    }
}