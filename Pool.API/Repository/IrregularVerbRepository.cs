using System.Data;
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
    }
    
    public async Task<IrregularVerbModel> GetRandomIrregularVerb()
    {
        try
        {
            using (var conn = new SqlConnection(_defaultConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select top 1 * from Irreg_Verbs  ORDER BY NEWID()", conn);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    IrregularVerbModel verb = new IrregularVerbModel()
                    {
                        Id = Int32.Parse(reader["Id"].ToString()),
                        Infinitive = reader["Infinitive"].ToString(),
                        PastSimple = reader["Past_simple"].ToString(),
                        PastParticiple = reader["Past_participle"].ToString(),
                        Translation = reader["Translation"].ToString(),
                       
                    };

                    return verb;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}