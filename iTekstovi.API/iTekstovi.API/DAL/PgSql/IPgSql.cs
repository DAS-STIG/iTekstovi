using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace iTekstovi.API.DAL.PgSql
{
    /// <summary>
    /// Contains methods for retrieving data from a PostgreSql database. All Sql calls
    /// are done through stored procedures called out in the objects properties
    /// </summary>
    public interface IPgSql 
    {
        #region PgSql Properties

        /// <summary>
        /// Dictionary with key of Model type and value of stored procedure name to retrive a 
        /// list of objects from the table associated with the Model type key
        /// </summary>
        IDictionary<Type, PgSqlFunction> ListProcedures { get; } 

        /// <summary>
        /// Dictionary with key of Table Name and value of stored procedure name to retrive a 
        /// single  object from the key Table Name
        /// </summary>
        IDictionary<Type, PgSqlFunction> GetProcedures { get; }

        /// <summary>
        /// Dictionary with key of Table Name and value of stored procedure name to save (upsert)
        /// object in the key Table Name
        /// </summary>
        IDictionary<Type, PgSqlFunction> SaveProcedures { get; }

         /// <summary>
        /// Dictionary with key of Table Name and value of stored procedure name to delete a 
        /// single object from the key Table Name
        /// </summary>
        IDictionary<Type, PgSqlFunction> DeleteProcedures { get; }

        #endregion
        
        #region PgSql Methods

        /// <summary>
        /// Executes a nonquery sql statement and returns boolean if successful 
        /// </summary>
        /// <param name="pgSqlFunction"></param>
        /// <returns></returns>
        Task<int> ExecuteNonQuery(PgSqlFunction pgSqlFunction);

        /// <summary>
        /// Executes a nonquery sql statement and returns boolean if successful 
        /// </summary>
        /// <param name="command">stored procedure name</param>
        /// <param name="paramz">optional params</param>
        /// <returns></returns>
        Task<int> ExecuteNonQuery(string command, List<NpgsqlParameter> paramz = null);

        /// <summary>
        /// Gets an object from T table corresponding to T id as sqlParam
        /// </summary>
        /// <param name="pgFunction"></param>
        /// <param name="sqlParam"></param>
        /// <returns></returns>
        Task<T> GetObject<T>(string pgFunction, NpgsqlParameter sqlParam) 
            where T : class;
        
        /// <summary>
        /// Gets a single row from the database and casts to class T
        /// </summary>
        /// <param name="pgFunction">stored procedure name</param>
        /// <param name="paramz">optional list of params</param>
        /// <returns></returns>
        Task<T> GetDataRow<T>(string pgFunction, List<NpgsqlParameter> paramz = null) 
            where T : class;

        /// <summary>
        /// Retrieves a list of objects of type T from the database 
        /// Overload of GetDataList that has PgSqlFunction as a paramter instead of name 
        /// and parameter list
        /// </summary>
        /// <param name="pgSqlFunction"></param>
        /// <returns></returns>
        Task<List<T>> GetDataList<T>(PgSqlFunction pgSqlFunction)
            where T : class;

        /// <summary>
        /// Retrieves a list of objects of type T from the database 
        /// Overload of GetDataList that has PgSqlFunction as a paramter instead of name 
        /// and parameter list
        /// </summary>
        /// <param name="pgSqlFunction"></param>
        /// <returns></returns>
        Task<List<T>> GetDataList<T>(string pgFunction, List<NpgsqlParameter> paramz = null) 
        where T : class;

        #endregion
    }
}
