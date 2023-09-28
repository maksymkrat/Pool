using Pool.Shared.Models;

namespace Pool.API.Services.IServicec;

public interface IWordService
{
    Task<IEnumerable<WordModel>> GetAllUsersWords(Guid userId);
    Task<IEnumerable<WordModel>> SearchWords(string word, Guid userId);
    Task<IEnumerable<WordModel>> GetFourRandomWords(Guid userId);
    Task<WordModel> GetRandomWord(Guid userId);
    Task<bool> DeleteWordById(int id);
    Task<bool> AddWord( WordModel word);
    Task<bool> Update( WordModel word);
}