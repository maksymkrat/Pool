using Microsoft.AspNetCore.Components;
using Pool.Client.Components;
using Pool.Client.Services;
using Pool.Shared.Models;
using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.Client.Pages;

public class Vocabulary_razor : ComponentBase
{
    [Inject] private AccountService _accountService { get; set; }
    [Inject] private ILogger<Vocabulary> _logger { get; set; }
    [Inject] private WordService _wordService { get; set; }

    private List<Word> _words = new List<Word>();

    protected List<Word> Words
    {
        get => _words;
        set
        {
            _words = value;
            InvokeAsync(StateHasChanged);
        }
    }

    protected string NewWord{ get; set; }
    protected string NewTranslate{ get; set; }
    protected string WordTranslation{ get; set; }
    protected string WordForTranslation{ get; set; }
    protected Translater Translater{ get; set; }
    protected DeleteConfiraation DeleteConfirmation { get; set; }
    protected UpdateWord UpdatedWord { get; set; }
    protected Word WordToBeDeleted { get; set; }
    protected Word WordToBeUpdated { get; set; }
    protected readonly Guid userId = new Guid("23A2DCB7-38B5-44B4-85E9-9E6AF7F4646F");


    protected override void OnInitialized()
    {
        UpdateWords();
    }

    protected void UpdateWords()
    {
        // var userId = _accountService.UserSession.Id;
        Words = _wordService.GetAllWords(userId); //hardcode
        InvokeAsync(StateHasChanged);
    }

    protected async void AddWord()
    {
        if (!string.IsNullOrWhiteSpace(NewWord) && !String.IsNullOrWhiteSpace(NewTranslate))
        {
            var result = await _wordService.AddWord(new Word()
            {
                WordText = NewWord.ToLower(),
                Translation = NewTranslate.ToLower(),
                User_id = userId
            });

            NewWord = string.Empty;
            NewTranslate = string.Empty;
            WordTranslation = string.Empty;
            WordForTranslation = string.Empty;
            if (result)
            {
                UpdateWords();
                InvokeAsync(StateHasChanged);
            }
            else
            {
                //  do not add word
            }
        }
    }

    protected void TranslateWord()
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: TranslateWord");
        if (!string.IsNullOrWhiteSpace(NewWord))
        {
            Translater = _wordService.Translate(NewWord);
            WordTranslation = Translater.Target.Text;
            NewTranslate = Translater.Target.Text;
            WordForTranslation = Translater.Source.Text;
        }
    }

    protected void DeleteWord(Word word)
    {
        WordToBeDeleted = word;
        DeleteConfirmation.Show();
    }

    protected void UpdateWord(Word word)
    {
        WordToBeUpdated = (Word) word.Clone();
        UpdatedWord.Show();
    }

    protected async void ConfirmDelete()
    {
        await _wordService.DeleteById(WordToBeDeleted.Id);
        DeleteConfirmation.Hide();
        UpdateWords();
        InvokeAsync(StateHasChanged);
        WordToBeDeleted = null;
    }

    protected void ConfirmUpdate()
    {
        _wordService.UpdateWord(WordToBeUpdated);

        UpdatedWord.Hide();
        UpdateWords();
        WordToBeUpdated = null;
        InvokeAsync(StateHasChanged);
    }

    protected void onCancel()
    {
        UpdatedWord.Hide();
        DeleteConfirmation.Hide();
        WordToBeDeleted = null;
        WordToBeUpdated = null;
    }

    protected void SayWord(Word word)
    {
        // _business.Speech(word.WordText);
    }
}