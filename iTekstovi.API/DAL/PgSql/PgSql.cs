using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using iTekstovi.API.App_Classes;

namespace iTekstovi.API.DAL.PgSql
{
    /// <summary>
    /// Contains methods for retrieving data from a PostgreSql database. All Sql calls
    /// are done through stored procedures called out in the objects properties
    /// </summary>
    public class PgSql : PgSqlObjects, IPgSql
    {
        /// <summary>
        /// PostgreSql Connection String
        /// </summary>
        /// <returns></returns>
        private string _connection 
        {
            get 
            {
                // add connection string here
                return "";
            }
        } 

        /// <summary>
        /// Creates an NpgsqlParameter with type, name, and value passed in 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="paramName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static NpgsqlParameter NpgParam(NpgsqlDbType dbType, string paramName, object val = null) 
        {
            var param = new NpgsqlParameter(paramName, dbType);
            // add value if set 
            if (val != null)
                param.Value = val;

            return param;
        }

        /// <summary>
        /// Creates a NpgSqlCommand of type Stored Procedure with name and optional parameters
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="command"></param>
        /// <param name="paramz"></param>
        /// <returns></returns>
        public static NpgsqlCommand NpgSqlCommand(NpgsqlConnection connection, string command, List<NpgsqlParameter> paramz = null)
        {
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            
            // pass in the inputed sql command or stored procedure 
            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // if there are any parameters add them
            if (paramz != null && paramz.Any()) 
            {
                foreach (NpgsqlParameter param in paramz)
                    cmd.Parameters.Add(param);
            }

            return cmd;
        }

        /// <summary>
        /// Executes a nonquery sql statement and returns boolean if successful 
        /// </summary>
        /// <param name="pgSqlFunction"></param>
        /// <returns></returns>
        public virtual async Task<int> ExecuteNonQuery(PgSqlFunction pgSqlFunction) 
        {
            return await ExecuteNonQuery(pgSqlFunction.Name, pgSqlFunction.Parameters.ToListOrNull());
        }

        /// <summary>
        /// Executes a nonquery sql statement and returns boolean if successful 
        /// </summary>
        /// <param name="command">stored procedure name</param>
        /// <param name="paramz">optional params</param>
        /// <returns></returns>
        public virtual async Task<int> ExecuteNonQuery(string command, List<NpgsqlParameter> paramz = null) 
        {
            int rowsAffected = 0;

            using (var pgCon = new NpgsqlConnection(this._connection)) 
            {
                pgCon.Open();
                
                try 
                {
                    NpgsqlCommand cmd = NpgSqlCommand(pgCon, command, paramz);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                           rowsAffected = reader[0] != null && reader.FieldCount > 0 ? 
                                            reader[0].ToIntOr(0) : 0;
                        }
                    } // end using DbDataReader 
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
                finally
                {
                    if (pgCon.State == ConnectionState.Open)
                        pgCon.Close();
                }
            } // end using NpgsqlConnection

            return rowsAffected; 
        }
        
        /// <summary>
        /// Gets an object from T table corresponding to T id as sqlParam
        /// </summary>
        /// <param name="pgFunction"></param>
        /// <param name="sqlParam"></param>
        /// <returns></returns>
        public virtual async Task<T> GetObject<T>(string pgFunction, NpgsqlParameter sqlParam)
            where T: class
        {
            T obj = null;

            var paramz = new List<NpgsqlParameter>();
            if (sqlParam != null)
            {
                paramz.Add(sqlParam);
                
                obj = await GetDataRow<T>(pgFunction, paramz);
            }
            
            return obj;
        }

        /// <summary>
        /// Gets a single row from the database and casts to class T
        /// </summary>
        /// <param name="pgFunction">stored procedure name</param>
        /// <param name="paramz">optional list of params</param>
        /// <returns></returns>
        public virtual async Task<T> GetDataRow<T>(string pgFunction, List<NpgsqlParameter> paramz = null) 
            where T : class
        {
            T obj = null; 

            var result = await GetDataList<T>(pgFunction, paramz);
            if (result.Any()) 
            {
                obj = result[0];
            }

            return obj;
        }

        /// <summary>
        /// Retrieves a list of objects of type T from the database 
        /// Overload of GetDataList that has PgSqlFunction as a paramter instead of name 
        /// and parameter list
        /// </summary>
        /// <param name="pgSqlFunction"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetDataList<T>(PgSqlFunction pgSqlFunction)
            where T : class
        {
            return await GetDataList<T>(pgSqlFunction.Name, pgSqlFunction.Parameters.ToListOrNull(removeNulls: false));
        }

        /// <summary>
        /// Retrieves a list of objects of type T from the database 
        /// </summary>
        /// <param name="pgFunction">stored procedure name</param>
        /// <param name="paramz">optional params</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetDataList<T>(string pgFunction, List<NpgsqlParameter> paramz = null) 
            where T : class
        {
            // list of generics for return
            List<T> allObjects = new List<T>(); 
            
            using (var pgCon = new NpgsqlConnection(this._connection)) 
            {
                pgCon.Open();
                
                try 
                {
                    NpgsqlCommand cmd = NpgSqlCommand(pgCon, pgFunction, paramz);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            T listObj = reader.ToModel<T>();
                            if (listObj != null)
                                allObjects.Add(listObj);
                        }
                    } // end using DbDataReader 
                }
                catch (Exception ex) 
                {
                    // ToDo: Add logger
                    throw ex; 
                }
                finally 
                {
                    if (pgCon.State == ConnectionState.Open)
                        pgCon.Close();
                }
            } // end using NpgsqlConnection 

            return allObjects;
        }
    }
}
