using Dapper;
using System.Data;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace GrowByData.Services
{
    public interface IDapperr:IDisposable
    {
        DbConnection GetDbConnection();
        T Get<T>(string sp, DynamicParameters prams, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetList<T>(string sp, DynamicParameters prams, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
