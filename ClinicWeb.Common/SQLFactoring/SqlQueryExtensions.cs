namespace ClinicWeb.Common.SQLFactoring
{
    public static class SqlQueryExtensions
    {
        internal static string GetShortName(string longName)
        {
            var type = Type.GetType(longName);

            if (type == typeof(byte))
                return "byte";
            if (type == typeof(sbyte))
                return "sbyte";
            if (type == typeof(short))
                return "short";
            if (type == typeof(ushort))
                return "ushort";
            if (type == typeof(int))
                return "int";
            if (type == typeof(uint))
                return "uint";
            if (type == typeof(long))
                return "long";
            if (type == typeof(ulong))
                return "ulong";
            if (type == typeof(byte))
                return "byte";
            if (type == typeof(char))
                return "char";
            if (type == typeof(float))
                return "float";
            if (type == typeof(double))
                return "double";
            if (type == typeof(decimal))
                return "decimal";
            if (type == typeof(bool))
                return "bool";
            if (type == typeof(string))
                return "string";
            if (type == typeof(byte[]))
                return "byte[]";
            if (type == typeof(DateTime))
                return "DateTime?";

            if (type == typeof(object))
                return "object";

            return longName;
        }
    }
}
