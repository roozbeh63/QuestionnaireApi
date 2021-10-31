using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Helpers
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Convert enum to a dictionary of name and id of the enum
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetValues(typeof(T))
.Cast<T>()
.ToDictionary(t => (int)(IConvertible)t, t => t.ToString());
        }

        public static string TypeCodeToSqlType(this TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return "BIT";

                case TypeCode.Char:
                    return "CHAR";

                case TypeCode.Byte:
                    return "BIT";

                case TypeCode.Int32:
                    return "INT";

                case TypeCode.UInt32:
                    return "INT";

                case TypeCode.Int64:
                    return "BIGINT";

                case TypeCode.UInt64:
                    return "BIGINT";

                case TypeCode.Double:
                    return "FLOAT";

                case TypeCode.DateTime:
                    return "DATETIME";

                case TypeCode.String:
                    return "VARCHAR";

                default:
                    return "VARCHAR(MAX)";
            }
        }
    }
}