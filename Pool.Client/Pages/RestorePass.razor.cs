using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Pool.Client.Pages
{
    public partial class RestorePass : ComponentBase
    {
        [Inject]  NavigationManager navManager { get; set; }

        protected bool Sent { get; set; }
        protected bool TrueCode { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public async Task VerifyCode()
        {
            if (!string.IsNullOrEmpty(Code))
            {
                // query verifyCode 
                TrueCode = true;
                await InvokeAsync(StateHasChanged);

            }
            else 
            {
               
            }
           
        }
        public async Task SendCode()
        {
            if (!string.IsNullOrEmpty(Email))
            {
               
                Sent = true;
                await InvokeAsync(StateHasChanged);
            }
            else 
            {
              
            }
        }

        public async Task ChangePassword()
        {
            if (!string.IsNullOrEmpty(Password) && Password.Equals(ConfirmPassword))
            {
                //update user password
            }
            
        }

        public void LogIn()
        {
            navManager.NavigateTo("/Login", true);
        }
    }
}