using Pool.API.Repository.IRepository;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;
    
    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public Task<List<Word>> GetAllUsersWords()
    {
        return _wordRepository.GetAllUsersWords();
    }

    public Task<List<Word>> GetFourRandomWords(Guid userId)
    {
        return _wordRepository.GetFourRandomWords(userId);
    }

    public Task<Word> GetRandomWord(Guid userId)
    {
        return _wordRepository.GetRandomWord(userId);
    }

    public Task<bool> DeleteWordById(int id)
    {
        return _wordRepository.DeleteWordById(id);
    }

    public Task<bool> AddWord(Word word)
    {
        return _wordRepository.AddWord(word);
    }
}