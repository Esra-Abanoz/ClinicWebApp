using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ClinicWeb.Common.Utils
{
    public class PluginRunner
    {
        private static readonly Lazy<PluginRunner> _instance = new Lazy<PluginRunner>(() => new PluginRunner());
        public static PluginRunner Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        public PluginRunner()
        {
            GetBytes();
            LoadMethodInfo();
        }
        private const string _providerClass = "SecretConnect.DotNet7Provider.SecretKey";
        private const string _resourceManifest = "ClinicWeb.Common.Build.S.C.P.dll";
        private const string prefixPattern = "0x66, 0xFE, 0x7D, 0x1E, 0x94, 0x00, 0x15, 0x6A, 0x42, 0x44, 0xD8, 0x13, 0xA7, 0x8D, 0x1E, 0x79, 0x46, 0xDB, 0x9A, 0xEE, 0xF9, 0x25, 0x3B, 0xC9, 0xGP, 0x5D, 0x0C, 0xE0, 0x7C, 0xC7, 0xF6, 0x32, 0xFY, 0xCC, 0x35, 0xA9, 0x06, 0x81, 0xBD, 0x07, 0x0D, 0x84, 0x3A, 0x45, ";
        private const string suffixPattern = "0x28, 0x00, 0xE1, 0x19, 0x6D, 0x9F, 0x70, 0xF0, 0x20, 0xA0, 0x86, 0xFD, 0x3B, 0xDT, 0x19, 0x86, 0x57, 0xB3, 0x6B, 0xF1, 0x6C, 0xHE, 0xBF, 0x91, 0xA3, 0xC5, 0xE4, 0x71, 0x1F, 0x67, 0x33, 0x82, 0xF0, 0xC2, 0x1D, 0x54, 0x8F, 0xC5, 0xYD, 0xB8, 0x19, 0x31, 0xDD, 0x0C, ";
        private object _lockObjectBytes = new object();
        private object _lockObjectMethodInfoList = new object();
        private byte[] _bytes;
        private MethodInfo _LoadAssembly;
        private MethodInfo _LoadAssemblyEncryptString;
        private MethodInfo _LoadAssemblyDecryptString;
        private void GetBytes()
        {
            lock (_lockObjectBytes)
            {
                try
                {
                    if (_bytes != null && _bytes.Length > 0)
                    {
                        return;
                    }
                    var entryAssembly = typeof(PluginRunner).Assembly;
                    var resourceStream = entryAssembly.GetManifestResourceStream(_resourceManifest);
                    byte[] bytes;
                    using (var streamReader = new MemoryStream())
                    {
                        resourceStream.CopyTo(streamReader);
                        bytes = streamReader.ToArray();
                    }
                    string confuse = Encoding.UTF8.GetString(bytes);
                    bytes = null;
                    var pattern = string.Concat(prefixPattern, "\\s*(?<value>.* ?)\\s*, ", suffixPattern);
                    if (!Regex.IsMatch(confuse, pattern))
                        throw new ArgumentException("S.C.P.dll is break;");

                    var hamveri = Regex.Match(confuse, pattern).Groups["value"];
                    var dizi = hamveri.ToString().Split(' ');
                    _bytes = dizi.Select(s => Convert.ToByte(s.Replace(",", string.Empty), 16)).ToArray();
                }
                catch (Exception e)
                {
                }
            }
        }
        private void LoadMethodInfo()
        {
            lock (_lockObjectMethodInfoList)
            {
                try
                {
                    if (_bytes == null || _bytes.Length <= 0)
                    {
                        GetBytes();
                    }
                    Assembly assembly = Assembly.Load(_bytes);
                    Type typeToExecute = assembly.GetType(_providerClass);
                    MethodInfo[] methods = typeToExecute.GetMethods();
                    foreach (MethodInfo item in methods)
                    {
                        switch (item.Name)
                        {
                            case "SetKeyGetModel":
                                _LoadAssembly = item;
                                break;
                            case "EncryptString":
                                _LoadAssemblyEncryptString = item;
                                break;
                            case "DecryptString":
                                _LoadAssemblyDecryptString = item;
                                break;

                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
        public dynamic LoadAssembly(string key)
        {
            if (_LoadAssembly == null)
            {
                LoadMethodInfo();
            }
            if (_LoadAssembly != null)
            {
                return _LoadAssembly.Invoke(null, new object[] { key });
            }
            return string.Empty;
        }
        public dynamic LoadAssemblyEncryptString(string key)
        {
            if (_LoadAssemblyEncryptString == null)
            {
                LoadMethodInfo();
            }
            if (_LoadAssemblyEncryptString != null)
            {
                return _LoadAssemblyEncryptString.Invoke(null, new object[] { key });
            }
            return string.Empty;
        }
        public dynamic LoadAssemblyDecryptString(string key)
        {
            if (_LoadAssemblyDecryptString == null)
            {
                LoadMethodInfo();
            }
            if (_LoadAssemblyDecryptString != null)
            {
                return _LoadAssemblyDecryptString.Invoke(null, new object[] { key });
            }
            return string.Empty;
        }
    }
}
