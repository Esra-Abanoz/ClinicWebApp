using Dapper;
using System.Security.Cryptography;
using System.Text;

namespace ClinicWeb.Common.Utils
{
    public static  class OtherFunctions
    {
        public static string ToMd5(this string item)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] btr = Encoding.UTF8.GetBytes(item);
            btr = md5.ComputeHash(btr);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in btr)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        public static string getTableName<T>(T obj)
        {
           // obj = Activator.CreateInstance<T>();
            return obj.GetType().Name;
        }
        public static Stream GenerateStreamFromString(string s, string fileName)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            var ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Plain);
            var attach = new System.Net.Mail.Attachment(stream, ct);
            attach.ContentDisposition.FileName = fileName;

            return stream;
        }
      
        public static string GetModelStringBuilderMonitor(string query, DynamicParameters param) //where T : class
        {
            var builder = new StringBuilder(query);
            if (param != null && param.ParameterNames.Any())
            {
                foreach (var pi in param.ParameterNames)
                {

                    var val = param.Get<object>(pi) ?? "NULL";
                    switch (val)
                    {
                        case IEnumerable<object> enumerable:
                            {
                                bool isNumber = false;

                                var ayrac = isNumber ? "" : "'";
                                string tmp = "";
                                foreach (var o in enumerable)
                                {
                                    if (o != null)
                                    {
                                        tmp += ayrac + o + ayrac + ",";
                                    }
                                    else
                                    {
                                        tmp += "NULL" + ",";
                                    }
                                }

                                tmp = tmp.TrimEnd(',');
                                builder.Replace(string.Concat("@", pi), string.Concat("ARRAY[", tmp, "]"));
                                break;
                            }
                        case Array array:
                            {
                                bool isNumber = false;
                                var ayrac = isNumber ? "" : "'";
                                var tmp = string.Empty;
                                foreach (var o in array)
                                {
                                    if (o != null)
                                    {
                                        tmp += ayrac + o + ayrac + ",";
                                    }
                                    else
                                    {
                                        tmp += "NULL" + ",";
                                    }
                                }

                                tmp = tmp.TrimEnd(',');
                                builder.Replace(string.Concat("@", pi), string.Concat("ARRAY[", tmp, "]"));
                                break;
                            }
                        case DateTime _:
                            builder.Replace(string.Concat("@", pi), $"'{val:s}'");
                            break;
                        case string _:
                            builder.Replace(string.Concat("@", pi), string.Concat("'", val.ToString(), "'"));
                            break;
                        default:
                            builder.Replace(string.Concat("@", pi), val.ToString());
                            break;
                    }

                }

            }
            return builder.ToString();
        }
        private static bool IsNumber(this object e)
        {
            switch (Type.GetTypeCode(e.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
