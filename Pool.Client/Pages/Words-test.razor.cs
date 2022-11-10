using Microsoft.AspNetCore.Components;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Words_test_razor : ComponentBase
{
    [Inject] private WordService _wordService { get; set; }
    protected Word MainWord { get; set; }
    protected List<Word> Words{ get; set; }
    protected Random rnd = new Random();
    protected string resultStyle = "light";
    protected string mainWordStyle = "light";
    protected int rndWord = 0;
    private readonly Guid userId = new Guid("23a2dcb7-38b5-44b4-85e9-9e6af7f4646f");


    protected  override void OnInitialized()
    {
      
        Words =  _wordService.GetFourRandomWords(userId);
        MainWord = Words.ElementAt(rnd.Next(0, Words.Count));
    }

    
    

    protected void ChangeStyleMainWord(string style)
    {
        mainWordStyle = style;
        
    }

    protected  void ResetWord()
    {
        Words =  _wordService.GetFourRandomWords(userId);
        MainWord = Words.ElementAt(rnd.Next(0, Words.Count));
        resultStyle = "light";
        mainWordStyle = resultStyle;
    }
}