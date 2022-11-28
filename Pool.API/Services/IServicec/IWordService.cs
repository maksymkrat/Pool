using Pool.Shared.Models;

namespace Pool.API.Services.IServicec;

public interface IWordService
{
    Task<List<WordModel>> GetAllUsersWords(Guid userId);
    Task<List<WordModel>> SearchWords(string word);
    Task<List<WordModel>> GetFourRandomWords(Guid userId);
    Task<WordModel> GetRandomWord(Guid userId);
    Task<bool> DeleteWordById(int id);
    Task<bool> AddWord( WordModel word);
    Task<bool> Update( WordModel word);
}