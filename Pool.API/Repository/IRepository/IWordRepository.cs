using Pool.Shared.Models;

namespace Pool.API.Repository.IRepository;

public interface IWordRepository
{
    Task<List<Word>> GetAllUsersWords(); //Guid userId add in parametrs
    Task<bool> AddWord( Word word,Guid userId);
    Task<bool> DeleteWordById(int id);
    Task<List<Word>> GetFourRandomWords(Guid userId);
    Task<Word> GetRandomWord(Guid userId);
    void UpdateWord(Word word,Guid userId);
    
}