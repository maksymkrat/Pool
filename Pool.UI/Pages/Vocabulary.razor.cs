using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Pool.Shared.Models;
using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.UI.Pages;

public class Vocabulary_razor : ComponentBase
{
     [Inject] private ILogger<Vocabulary> _logger { get; set; }
        // [Inject] private IWordRepository _repository { get; set; }
        // [Inject] private IBusinessLogic _business { get; set; }

        protected List<Word> words = new List<Word>();

        protected List<Word> Words
        {
            get => words;
            set
            {
                words = value;
                InvokeAsync(StateHasChanged);
            }
        }

        protected string newWord;
        protected string newTranslate;
        protected Translater translater;
        protected string wordTranslation;
        protected string WordForTranslation;
        //protected Confirmation Confirmation { get; set; }
        protected Word WordToBeDelated { get; set; }

        protected async void AddWord()
        {
            if (!string.IsNullOrWhiteSpace(newWord) && !String.IsNullOrWhiteSpace(newTranslate))
            {
                // var result = await _repository.AddWord(new Word()
                // {
                //     WordText = newWord.ToLower(),
                //     Translation = newTranslate.ToLower()
                // });

                newWord = string.Empty;
                newTranslate = string.Empty;
                wordTranslation = string.Empty;
                // if (result)
                // {
                //     UpdateWords();
                //     StateHasChanged();
                // }
                // else
                // {
                //     //  do not add word
                // }
            }
        }

        protected void TranslateWord()
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: TranslateWord");
            // if (!string.IsNullOrWhiteSpace(newWord))
            // {
            //     translater = _business.Translate(newWord);
            //     wordTranslation = translater.Target.Text;
            //     newTranslate = translater.Target.Text;
            //     WordForTranslation = translater.Source.Text;
            // }
        }

        // protected void DeleteWord(Word word)
        // {
        //     WordToBeDelated = word;
        //     Confirmation.Show();
        // }

        // protected void onConfirm()
        // {
        //     _repository.DeleteWordById(WordToBeDelated.Id);
        //     Confirmation.Hide();
        //     UpdateWords();
        //     StateHasChanged();
        //     WordToBeDelated = null;
        // }

        // protected void onCancel()
        // {
        //     Confirmation.Hide();
        //     WordToBeDelated = null;
        // }

        protected void SayWord(Word word)
        {
           // _business.Speech(word.WordText);
        }

        protected override void OnInitialized()
        {
            UpdateWords();
        }

        protected async void UpdateWords()
        {
            // Words = await _repository.GetAllWords();
            // StateHasChanged();
        }
}