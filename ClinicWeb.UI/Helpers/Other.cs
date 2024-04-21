namespace ClinicWeb.UI.Helpers
{
    public static class OtherCommon
    {
        public static string NotifMessage(string message , NotifyType type)
        {
            switch (type)
            {
                case NotifyType.successNotif:
                    return $"<div class='alert-box success'> &nbsp;{message} </div>";
                case NotifyType.errorNotif:
                    return $"<div class='alert-box error'> &nbsp; {message} </div>";
                case NotifyType.confirmedNotif:
                    return $"<div class='alert-box warning'> &nbsp; {message} </div>";
                default:
                    return $"<div class='alert-box success'> &nbsp; {message} </div>";
            }
        }
        public static long IP2Long(string ip)
        {
            string[] ipBytes;
            double num = 0;
            if (!string.IsNullOrEmpty(ip))
            {
                ipBytes = ip.Split('.');
                for (int i = ipBytes.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                }
            }
            return (long)num;
        }
    }
}