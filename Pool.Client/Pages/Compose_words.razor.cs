using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Authentication;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Compose_words_razor : ComponentBase
{
    [Inject] private WordService _wordService { get; set; }
    [Inject] private AuthenticationStateProvider  _authStateProvider { get; set; }
    protected string ResultStyle { get; set; }
    protected Random random = new Random();
    protected Word mainWord{ get; set; }
    protected List<char> resultWord = new List<char>();
    protected List<char> mixedСharacters = new List<char>();
    


    protected  override void OnInitialized()
    {
        var userId = ((CustomAuthenticationStateProvider)_authStateProvider).UserSession.Id;
        mainWord =  _wordService.GetRandomWord(userId);
        char[] arrayChars = mainWord.WordText.ToCharArray();
        mixedСharacters = arrayChars.OrderBy(x => random.Next()).ToList();
        resultWord.Clear();
        ResultStyle = "light";
    }
    
    
    
    protected void ResetWord()
    {
       OnInitialized();
    }
    
    protected void AddCharForResult(char selectChar)
    {
        resultWord.Add(selectChar);
        mixedСharacters.Remove(selectChar);
        ResultStyle = "light";
        if (mixedСharacters?.Count == 0)
        {
            CompareWords();
            InvokeAsync(StateHasChanged);
        }
    }
    
    protected void DeleteCharWithResult(char selectChar)
    {
        resultWord.Remove(selectChar);
        mixedСharacters.Add(selectChar);
        ResultStyle = "light";
           
    }
    
    protected void CompareWords()
    {
        StringBuilder blindWord = new StringBuilder();
           
        for (int i = 0; i < resultWord.Count; i++)
        {
            blindWord.Append(resultWord[i]);
        }
        if (blindWord.ToString().Equals(mainWord.WordText))
        {
            ResultStyle = "success";
        }else
        {
            ResultStyle = "danger";
        }
    }
}