using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Authentication;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Words_test_razor : ComponentBase
{
    [Inject] private WordService _wordService { get; set; }
    [Inject] private AuthenticationStateProvider  _authStateProvider { get; set; }
    protected Word MainWord { get; set; }
    protected List<Word> Words{ get; set; }
    protected Random rnd = new Random();
    protected string resultStyle = "light";
    protected string mainWordStyle = "light";
    protected int rndWord = 0;
    


    protected  override void OnInitialized()
    {
        var userId = ((CustomAuthenticationStateProvider)_authStateProvider).UserSession.Id;
        Words =  _wordService.GetFourRandomWords(userId);
        MainWord = Words.ElementAt(rnd.Next(0, Words.Count));
    }

    
    

    protected void ChangeStyleMainWord(string style)
    {
        mainWordStyle = style;
        
    }

    protected  void ResetWord()
    {
        OnInitialized();
        resultStyle = "light";
        mainWordStyle = resultStyle;
    }
}