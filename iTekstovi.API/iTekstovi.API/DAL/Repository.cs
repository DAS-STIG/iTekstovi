using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Npgsql;
using iTekstovi.API.DAL.PgSql;
using iTekstovi.API.AppClasses;
using Microsoft.Extensions.Options;

namespace iTekstovi.API.DAL
{
    /// <summary>
    /// Repository for Listing, Getting, Saving, or Deleting an object in a PostgreSql DB
    /// </summary>
    public interface IRepository<T> 
    {
        Task<List<T>> All(string id = null);

        Task<T> Get(string id);

        Task<bool> Save(T obj);

        Task<bool> Delete(string id);
    }

    /// <summary>
    /// Repository for Listing, Getting, Saving, or Deleting an object in a PostgreSql DB
    /// </summary>
    public class Repository<T> : IRepository<T> 
        where T : class
    {
        private IPgSql _PgSql { get; set; }

        public Repository(IOptions<ApiConfig> configValues)
        {
            _PgSql = new iTekstovi.API.DAL.PgSql.PgSql(configValues);
        }

        public Repository(IPgSql _pgSql)
        {
            _PgSql = _pgSql;
        }

        /// <summary>
        /// Get a List&lt;T&gt; of objects. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<T>> All(string id = null)
        {
            List<T> allObjects = new List<T>();
            PgSqlFunction pgSqlObject = _PgSql.ListProcedures.GetValOr(typeof (T));
            
            if (pgSqlObject != null)
            {
                // add the id parameter if it is set (only some objects require a list param)
                if (pgSqlObject.Parameters != null && !string.IsNullOrEmpty(id))
                {
                    pgSqlObject.Parameters[0].Value = id;
                }

                allObjects = await _PgSql.GetDataList<T>(pgSqlObject);                
            }

            return allObjects;
        }

        /// <summary>
        /// Get object of type T by id (string id parsed to integer or Guid 
        /// depending on Type T)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> Get(string id) 
        {
            T obj = null;
            PgSqlFunction pgSqlObject = _PgSql.GetProcedures.GetValOr(typeof (T));
                    
            if (!string.IsNullOrEmpty(id) && pgSqlObject != null && pgSqlObject.Parameters != null)
            {
                pgSqlObject.Parameters[0].Value = id;
                obj = await _PgSql.GetObject<T>(pgSqlObject.Name, pgSqlObject.Parameters[0]);
            }
            
            return obj;
        }

        /// <summary>
        /// Saves an object of type T in the DB
        /// Note: Uses Reflection to map Object proeprties to NpgSql Parameters 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> Save(T obj)
        {
            PgSqlFunction pgSqlObject = _PgSql.SaveProcedures.GetValOr(typeof (T));
            var paramz = new List<NpgsqlParameter>();

            if (obj != null && pgSqlObject != null) 
            {   
                IEnumerable<PropertyInfo> objProperties = obj.GetType().GetTypeInfo().DeclaredProperties;
                List<NpgsqlParameter> procedureParams = pgSqlObject.Parameters.ToList<NpgsqlParameter>(); 
                
                foreach(var property in objProperties)
                {
                    // get column name attribute from property                    
                    ColumnName columnNameAttr = property.GetCustomAttribute(typeof(ColumnName), false) as ColumnName;
                    string columnName = columnNameAttr !=null ? columnNameAttr.AttributeValue : null;

                    // match the column name to an obj property 
                    if (!string.IsNullOrEmpty(columnName) && 
                        columnName.Replace("_", "").Equals(property.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // find the parameter that matches the column name
                        var sqlParam = procedureParams.FirstOrDefault(p => p.ParameterName == ("p_" + columnName));

                        // add the param to the list of parameters
                        if (sqlParam != null && sqlParam.Value == null && !paramz.Contains(sqlParam)) 
                        {
                            // try and get the value from the object model
                            sqlParam.Value = property.GetValue(obj, null);
    
                            // only add the parameter if it has a value
                            if (sqlParam.Value != null)
                                paramz.Add(sqlParam);
                        }
                    } // end if columnName not empty && prop name = columnName
                } // end foreach property
            } // end obj != null && pgSqlObject != null

            return pgSqlObject != null && await _PgSql.ExecuteNonQuery(pgSqlObject.Name, paramz.ToList()) > 0;
        }

        /// <summary>
        /// Deletes an object of type T from the DB by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            bool status = false;
            PgSqlFunction pgSqlObject = _PgSql.DeleteProcedures.GetValOr(typeof (T));
            var paramz = new List<NpgsqlParameter>();

            // delete procedures take one parameter which is id of object to delete
            if (!string.IsNullOrEmpty(id) && pgSqlObject != null && pgSqlObject.Parameters != null)
            {
                pgSqlObject.Parameters[0].Value = id;
                paramz.Add(pgSqlObject.Parameters[0]);

                status = await _PgSql.ExecuteNonQuery(pgSqlObject) > 0;            
            }

            return status;
        }
    }
}