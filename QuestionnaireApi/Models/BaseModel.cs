using Microsoft.EntityFrameworkCore;
using QuestionnaireApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QuestionnaireApi.Models
{
    public abstract class BaseModel
    {
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Table name of the class
        /// </summary>
        [NotMapped]
        public string TableName
        {
            get
            {
                return this.GetType().GetCustomAttribute<TableAttribute>().Name;
            }
        }

        /// <summary>
        /// Primary keys of the class
        /// </summary>
        [NotMapped]
        private string PrimaryKey
        {
            get
            {
                /** get the property of the object which has key attribute */
                PropertyInfo[] properties = this.GetType().GetProperties().Where(pi => System.Attribute.IsDefined(pi, typeof(KeyAttribute))).ToArray();
                if (properties.Count() == 0)
                {
                    throw new Exception($"No property has found with the Key attribute for type ({this.GetType().Name})");
                }
                if (properties.Count() > 1)
                {
                    throw new Exception($"More than one primary key for the object({this.GetType().Name}) has been found");
                }
                PropertyInfo property = properties[0];
                if (System.Attribute.IsDefined(property, typeof(ColumnAttribute)))
                    /** return the property name as the primary key name */
                    return properties[0].GetCustomAttribute<ColumnAttribute>().Name;
                else
                    return properties[0].Name;
            }
        }

        /// <summary>
        /// Map values from given class to this class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_object"></param>
        public void MapValues<T>(T _object)
        {
            PropertyInfo[] properties = typeof(T).GetProperties().Where(pi => !System.Attribute.IsDefined(pi, typeof(NotMappedAttribute))).ToArray();
            properties.ToList().ForEach(prop =>
            {
                prop.SetValue(this, prop.GetValue(_object));
            });
        }

        /// <summary>
        /// Create table if not exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        public async Task CreateTableIfNotExists<T>(DbContext db)
        {
            string sql = $@"
IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{TableName}]') AND type in (N'U'))
				BEGIN
					CREATE TABLE [dbo].[{TableName}](
					{PrimaryKey} BIGINT NOT NULL IDENTITY (1,1) PRIMARY KEY,
					{string.Join(",", GetGenerateFieldsQuery<T>().Select(kvp => kvp.Key + "  " + kvp.Value))}
				)
				END
";
            await db.RawSqlQueryAsync(sql);
        }

        /// <summary>
        /// Get fields for creating a DLL script to generate the table in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private Dictionary<string, string> GetGenerateFieldsQuery<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties()
                .Where(pi => !System.Attribute.IsDefined(pi, typeof(NotMappedAttribute)) && !System.Attribute.IsDefined(pi, typeof(KeyAttribute))).ToArray();
            Dictionary<string, string> fields = new Dictionary<string, string>();
            foreach (PropertyInfo pi in properties)
            {
                System.TypeCode typeCode = Type.GetTypeCode(pi.PropertyType);
                /** if the attribute for colume name is defined then we use the attribute otherwise we use the property name itself */
                string colName = System.Attribute.IsDefined(pi, typeof(ColumnAttribute)) ? pi.GetCustomAttribute<ColumnAttribute>().Name : pi.Name;

                if (typeCode == TypeCode.String)
                {
                    string length = "MAX";
                    if (System.Attribute.IsDefined(pi, typeof(StringLengthAttribute)))
                        length = pi.GetCustomAttribute<StringLengthAttribute>().MaximumLength.ToString();
                    if (System.Attribute.IsDefined(pi, typeof(MaxLengthAttribute)))
                        length = pi.GetCustomAttribute<MaxLengthAttribute>().Length.ToString();

                    fields.Add(colName, $"VARCHAR({length})");
                }
                else if (typeCode == TypeCode.Object)
                {
                    if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        fields.Add(colName, Type.GetTypeCode(Nullable.GetUnderlyingType(pi.PropertyType)).TypeCodeToSqlType());
                    }
                }
                else
                {
                    fields.Add(colName, typeCode.TypeCodeToSqlType());
                }
            }
            return fields;
        }
    }
}