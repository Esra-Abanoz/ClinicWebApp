namespace ClinicWeb.Core.Extentions
{
    // Byte değeri türüne göre convert ediyor.
    public static class ConvertTypeSizeExtension
    {
        static readonly string[] suffixes =
            { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public static string ToSize(this long value)
        {
            int counter = 0;
            decimal number = (value*1000);
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }
    }

}
