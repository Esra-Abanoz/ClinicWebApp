using System.Net;
using System.Net.Sockets;

namespace ClinicWeb.UI.Extensions
{
    public static class HttpContextExtensions
    {
        private static bool IsIpV4Address(string ipAddress)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ipAddress, "((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.|$)){4}");
        }
        public static string GetClientIpAddress(this HttpContext httpContext)
        {
            string ipAddress = string.Empty;
            try
            {
                if (httpContext != null)
                {
                    var remoteIpAddress = httpContext.Connection.RemoteIpAddress;
                    if (remoteIpAddress != null)
                    {
                        var addressList = Dns.GetHostEntry(remoteIpAddress).AddressList.ToList();
                        if (addressList != null && addressList.Any())
                        {
                            remoteIpAddress = addressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
                            //remoteIpAddress = addressList.Last(x => x.AddressFamily == AddressFamily.InterNetwork);
                        }
                        else
                        {
                            remoteIpAddress = remoteIpAddress.MapToIPv4();
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            if (string.IsNullOrWhiteSpace(ipAddress) || !IsIpV4Address(ipAddress))
            {
                ipAddress = string.Empty;
            }
            return ipAddress;
        }
        public static List<string> GetClientIpAddressList(this HttpContext httpContext)
        {
            List<string> ipAdressList = new List<string>();
            try
            {
                if (httpContext != null)
                {
                    var remoteIpAddress = httpContext.Connection.RemoteIpAddress;
                    if (remoteIpAddress != null)
                    {
                        if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            var addressList = Dns.GetHostEntry(remoteIpAddress).AddressList.ToList();
                            if (addressList != null && addressList.Any())
                            {
                                ipAdressList = addressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString())
                                    .ToList();
                            }
                            else
                            {
                                ipAdressList.Add(remoteIpAddress.MapToIPv4().ToString());
                            }
                        }
                        else
                        {
                            ipAdressList.Add(remoteIpAddress.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            if (ipAdressList.Any())
            {
                ipAdressList = ipAdressList.Where(x => IsIpV4Address(x)).ToList();
            }
            return ipAdressList;
        }
    }
}
