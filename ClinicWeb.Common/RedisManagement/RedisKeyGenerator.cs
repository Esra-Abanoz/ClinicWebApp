using System.Text;

namespace ClinicWeb.Common.RedisManagement
{
    public class RedisKeyGenerator
    {
        public static string CreateWithParts(string objectTypeName, params string[] keyParts)
        {
            if (objectTypeName.Contains(":"))
                throw new ArgumentException("objectTypeName geçersiz karakterlere sahip olamaz: ':'");
            var stringBuilder = new StringBuilder();
            foreach (string keyPart in keyParts)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(':');
                stringBuilder.Append(keyPart);
            }
            return string.Format("{0}:{1}", objectTypeName, stringBuilder);
        }
    }
}
