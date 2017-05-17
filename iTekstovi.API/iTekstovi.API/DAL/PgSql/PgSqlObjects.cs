using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using iTekstovi.API.Models;
using iTekstovi.API.AppClasses;

namespace iTekstovi.API.DAL.PgSql
{
    /// <summary>
    /// Contains Dictionary&lt;Type, PgSqlFunction&gt; Properties for getting List, Get, Save, and Delete 
    /// PgSql Function names and parameters by Type of Model (DB Table)
    /// </summary>
    public class PgSqlObjects
    {
        /// <summary>
        /// Dictionary with key of Model type and value of stored procedure name to retrive a 
        /// list of objects from the table associated with the Model type key
        /// </summary>
        private IDictionary<Type, PgSqlFunction> _listProcedures = null;
        public IDictionary<Type, PgSqlFunction> ListProcedures 
        { 
            get 
            {
                if (_listProcedures == null)
                {
                    _listProcedures =  new Dictionary<Type, PgSqlFunction> 
                    {
                        { 
                            typeof (SongModel), new PgSqlFunction 
                            { 
                                Name = "itvi.list_song",
                                Parameters = new NpgsqlParameter[] { PgSql.NpgParam(NpgsqlDbType.Boolean, "p_only_visible" ) }
                            }
                            
                        },
                        {

                        typeof (ArtistModel), new PgSqlFunction
                            {
                                Name = "itvi.list_artist",
                                Parameters = new NpgsqlParameter[] { PgSql.NpgParam(NpgsqlDbType.Boolean, "p_only_visible" ) }
                            }
                        }
                    };
                } // end if _listProcedures == null
                                    
                return _listProcedures;
            }
        }
        
        /// <summary>
        /// Dictionary with key of Table Name and value of stored procedure name to retrive a 
        /// single  object from the key Table Name
        /// </summary>
        private IDictionary<Type, PgSqlFunction> _getProcedures = null;
        public IDictionary<Type, PgSqlFunction> GetProcedures 
        { 
            get 
            {
                if (_getProcedures == null) 
                {
                    _getProcedures = new Dictionary<Type, PgSqlFunction> 
                    {
                        { 
                            typeof(SongModel),  new PgSqlFunction 
                            {
                                Name = "itvi.get_song_by_id",
                                Parameters = new NpgsqlParameter[] { PgSql.NpgParam(NpgsqlDbType.Uuid, "p_id") }
                            } 
                        },
                        {
                            typeof(ArtistModel),  new PgSqlFunction
                            {
                                Name = "itvi.get_artist_by_id",
                                Parameters = new NpgsqlParameter[] { PgSql.NpgParam(NpgsqlDbType.Bigint, "p_id") }
                            }
                        }
                    };
                } // end if _getProcedures == null
                    
                return _getProcedures;
            }
        }

        /// <summary>
        /// Dictionary with key of Table Name and value of stored procedure name to save (upsert)
        /// object in the key Table Name
        /// </summary>
        private IDictionary<Type, PgSqlFunction> _saveProcedures = null;
        public IDictionary<Type, PgSqlFunction> SaveProcedures 
        { 
            get 
            {
                if (_saveProcedures == null)
                {
                    _saveProcedures = new Dictionary<Type, PgSqlFunction>
                    {
                        { 
                            typeof (SongModel), new PgSqlFunction 
                            {
                                Name = "save_song",
                                Parameters = new NpgsqlParameter[] 
                                {
                                    PgSql.NpgParam(NpgsqlDbType.Text, "p_name"),
                                    PgSql.NpgParam(NpgsqlDbType.Text, "p_description"),
                                    PgSql.NpgParam(NpgsqlDbType.Text, "p_lyrics"),
                                    PgSql.NpgParam(NpgsqlDbType.Bigint, "p_artist_id"),
                                    PgSql.NpgParam(NpgsqlDbType.Uuid, "p_id")
                                }
                            } 
                        },
                        {
                            typeof (ArtistModel), new PgSqlFunction
                            {
                                Name = "save_artist",
                                Parameters = new NpgsqlParameter[]
                                {
                                    PgSql.NpgParam(NpgsqlDbType.Text, "p_first_name"),
                                    PgSql.NpgParam(NpgsqlDbType.Text, "p_last1_name"),
                                    PgSql.NpgParam(NpgsqlDbType.Text, "p_about"),
                                    PgSql.NpgParam(NpgsqlDbType.Bigint, "p_id")
                                }
                            }
                        }
                    };
                } // end if _saveProcedures == null
                
                return _saveProcedures;
            }
        }

        /// <summary>
        /// Dictionary with key of Table Name and value of stored procedure name to delete a 
        /// single object from the key Table Name
        /// </summary>
        private IDictionary<Type, PgSqlFunction> _deleteProcedures = null;
        public IDictionary<Type, PgSqlFunction> DeleteProcedures 
        { 
            get
            {
                if (_deleteProcedures == null)
                {
                    _deleteProcedures = new Dictionary<Type, PgSqlFunction>
                    {
                        // add delete procedures here
                        { 
                            typeof (SongModel), new PgSqlFunction 
                            {
                                Name =  "delete_model",
                                Parameters = new NpgsqlParameter[] { PgSql.NpgParam(NpgsqlDbType.Uuid, "p_id") }
                            }
                        }
                        
                    };
                } // end if _deleteProcedures == null 

                return _deleteProcedures;
            }
        }
    }
}
