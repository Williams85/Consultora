using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Repositorio.BaseDatos
{
    public class Reader
    {
        public static object GetObjectValue(IDataReader dr, string column)
        {
            try
            {
                var obj = dr[column];
                return obj == DBNull.Value ? null : obj;
            }
            catch { throw; }
        }
        public static string GetStringValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                return obj == null ? null : obj.ToString();
            }
            catch { throw; }
        }
        public static decimal GetDecimalValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return 0;
                return Convert.ToDecimal(obj);
            }
            catch { throw; }
        }
        public static decimal? GetDecimalNullValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return null;
                return Convert.ToDecimal(obj);
            }
            catch { throw; }
        }

        public static double GetDoubleValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return 0;
                return Convert.ToDouble(obj);
            }
            catch { throw; }
        }
        public static DateTime GetDateTimeValue(IDataReader dr, string column)
        {
            try
            {
                var obj = dr[column];
                return obj == DBNull.Value ? DateTime.Now : (DateTime)obj;
            }
            catch { throw; }
        }
        public static DateTime? GetDateTimeNullValue(IDataReader dr, string column)
        {
            try
            {
                var obj = dr[column];
                return obj == DBNull.Value ? null : (DateTime?)obj;
            }
            catch { throw; }
        }
        public static int GetIntValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return 0;
                return Convert.ToInt32(obj);
            }
            catch { throw; }
        }
        public static Int16 GetSmallIntValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return 0;
                return Convert.ToInt16(obj);
            }
            catch { throw; }
        }
        public static byte GetTinyIntValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return 0;
                return Convert.ToByte(obj);
            }
            catch { throw; }
        }
        public static int? GetIntNullValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return null;
                return Convert.ToInt32(obj);
            }
            catch { throw; }
        }
        public static bool? GetBooleanValue(IDataReader dr, string column)
        {
            try
            {
                var obj = GetObjectValue(dr, column);
                if (obj == null) return null;
                return Convert.ToBoolean(obj);
            }
            catch { throw; }
        }

    }
}
