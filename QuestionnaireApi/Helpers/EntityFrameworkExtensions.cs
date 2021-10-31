using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Helpers
{
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Load the list of objects from Raw sql query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">db context</param>
        /// <param name="query">  the query to load data from</param>
        /// <param name="map">    </param>
        /// <returns></returns>
        public static IEnumerable<T> RawSqlQuery<T>(this DbContext context, string query, Func<DbDataReader, T> map)
        {
            using (DbCommand command = context.Database.Connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                context.Database.Connection.Open();

                using (DbDataReader result = command.ExecuteReader())
                {
                    List<T> entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }

        /// <summary>
        /// Load the list of objects from Raw sql query async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">db context</param>
        /// <param name="query">  the query to load data from</param>
        /// <param name="map">    </param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> RawSqlQueryAsync<T>(this DbContext context, string query, Func<DbDataReader, T> map)
        {
            using (DbCommand command = context.Database.Connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                context.Database.Connection.Open();

                using (DbDataReader result = await command.ExecuteReaderAsync())
                {
                    List<T> entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }

        /// <summary>
        /// Load datatable from raw data
        /// </summary>
        /// <param name="context">   db context</param>
        /// <param name="query">     query from which the data would be loaded</param>
        /// <param name="parameters">parameters for the sql</param>
        /// <returns></returns>
        public static async Task<DataTable> RawSqlQueryAsync(this DbContext context, string query, Dictionary<string, object> parameters = null)
        {
            return await ExecuteQueryAsync(context, query, CommandType.Text, parameters);
        }

        /// <summary>
        /// Execute store procedures
        /// </summary>
        /// <param name="context">   </param>
        /// <param name="query">     </param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteStoreProcedureAsync(this DbContext context, string query, Dictionary<string, object> parameters = null)
        {
            return await ExecuteQueryAsync(context, query, CommandType.StoredProcedure, parameters);
        }

        private static async Task<DataTable> ExecuteQueryAsync(this DbContext context, string query, CommandType commandType, Dictionary<string, object> parameters = null)
        {
            DataTable table = new DataTable();
            /** get the db command from context */
            using (DbCommand command = context.Database.Connection.CreateCommand())
            {
                /** set the query to the command */
                command.CommandText = query;
                /** the command type would be text */
                command.CommandType = commandType;
                /** load through the parameter and add them to command parameter */
                foreach (KeyValuePair<string, object> keyValue in parameters ?? new Dictionary<string, object>())
                {
                    DbParameter parameter = command.CreateParameter();
                    parameter.ParameterName = keyValue.Key;
                    parameter.Value = keyValue.Value;
                    command.Parameters.Add(parameter);
                }
                /** open the connection to the database */
                context.Database.Connection.Open();
                /** execute the command */
                using (DbDataReader result = await command.ExecuteReaderAsync())
                {
                    table.Load(result);
                    return table;
                }
            }
        }
    }
}