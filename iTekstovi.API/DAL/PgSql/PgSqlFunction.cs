using Npgsql;

namespace iTekstovi.API.DAL.PgSql
{
    /// <summary>
    /// Object with PostgreSql Function information with Name property of sql 
    /// function name and  Parameters property of type NpgsqlParameter[]
    /// </summary>
    public class PgSqlFunction
    {
        /// <summary>
        /// Name of PostgreSql Function
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// Optional arry of PostgreSql Parameters
        /// </summary>
        /// <returns></returns>
        public NpgsqlParameter[] Parameters { get; set; } 
    }
}