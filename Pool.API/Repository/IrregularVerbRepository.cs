using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Pool.API.Repository.IRepository;
using Pool.Shared.Models;

namespace Pool.API.Repository;

public class IrregularVerbRepository : IIrregularVerbRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _defaultConnection;

    public IrregularVerbRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _defaultConnection = _configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine(_defaultConnection);
    }
    
    public async Task<IrregularVerbModel> GetRandomIrregularVerb()
    {
        try
        {
            using (IDbConnection db = new SqlConnection(_defaultConnection))
            {
                var verbs =  await db.QueryAsync<IrregularVerbModel>(
                    "select top 1 Id, Infinitive, Past_simple as PastSimple, Past_participle as PastParticiple, Translation" +
                    " FROM Irreg_Verbs  ORDER BY NEWID()");
                return verbs.FirstOrDefault();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}