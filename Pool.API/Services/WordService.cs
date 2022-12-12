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

    public Task<List<WordModel>> GetAllUsersWords(Guid userId)
    {
        return _wordRepository.GetAllUsersWords(userId);
    }

    public Task<List<WordModel>> SearchWords(string word, Guid userId)
    {
        return _wordRepository.SearchWords(word, userId);
    }

    public Task<List<WordModel>> GetFourRandomWords(Guid userId)
    {
        return _wordRepository.GetFourRandomWords(userId);
    }

    public Task<WordModel> GetRandomWord(Guid userId)
    {
        return _wordRepository.GetRandomWord(userId);
    }

    public Task<bool> DeleteWordById(int id)
    {
        return _wordRepository.DeleteById(id);
    }

    public Task<bool> AddWord(WordModel word)
    {
        return _wordRepository.AddWord(word);
    }

    public Task<bool> Update(WordModel word)
    {
        return _wordRepository.Update(word);
    }
}