namespace ClinicWeb.Common.Utils
{
    public static class SifreAlgt
    {
        public static string Sifrele(string metin)
        {
            int a;
            string temp, karakter;
            temp = "";
            karakter = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyzxwXWqQ1234567890\"é!^+%&/()=?_*-@~ <>|.,:;#${[]}\\";

            for (int i = 0; i <= metin.Length - 1; i++)
            {
                a = karakter.IndexOf(metin[i], 1);

                if (a < 0)
                    a = 0;

                if ((a < (karakter.Length - 14)) && (a > 0))
                {
                    temp = temp + karakter[a + 15];
                }
                else if ((a > (karakter.Length - 14)) && (a != 0))
                {
                    a = a + 15;
                    temp = temp + karakter[i];
                }

                if (a == 0)
                {
                    temp = temp + metin[i];
                }
            }
            return temp;
        }

        public static string SifreCoz(string metin)
        {
            var temp = "";
            var karakter = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyzxwXWqQ1234567890\"é!^+%&/()=?_*-@~ <>|.,:;#${[]}\\";
            for (int i = 0; i <= metin.Length - 1; i++)
            {
                var a = karakter.IndexOf(metin[i], 1);

                if (a < 0)
                    a = 0;

                if (a > 15)
                {
                    temp = temp + karakter[a - 15];
                }
                else if ((a < 16) && (a != 0))
                {
                    a = a + karakter.Length;
                    temp = temp + karakter[a - 15];
                }

                if (a == 0)
                {
                    temp = temp + metin[i];
                }
            }

            return temp;
        }

        public static string DecodePwdEx(string data, string securityString)
        {
            string Codes64 = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";

            string result = "";

            if (securityString.Length < 16)
                return result;

            for (int i = 0; i < securityString.Length; i++)
            {
                var s1 = securityString.Substring(i + 1);

                if (s1.IndexOf(securityString[i]) > 0) { return result; }
                if (Codes64.IndexOf(securityString[i]) < 0) { return result; }
            }

            var s2 = "";
            var ss = securityString;

            for (int i = 0; i < data.Length; i++)
            {
                if (ss.IndexOf(data[i]) >= 0) { s2 = s2 + data[i]; }
            }

            data = s2;
            s2 = "";

            if (data.Length % 2 != 0)
            {
                return result;
            }

            for (int i = 0; i <= (data.Length / 2 - 1); i++)
            {
                var x = ss.IndexOf(data[(i * 2 + 1) - 1]);
                if (x < 0) { return result; }
                ss = ss.Substring(ss.Length - 1) + ss.Substring(0, ss.Length - 1);
                var x2 = ss.IndexOf(data[(i * 2 + 2) - 1]);
                if (x2 < 0) { return result; }
                x = x + x2 * 16;
                byte[] ara = new byte[1];
                ara[0] = (byte)x;
                s2 = s2 + System.Text.Encoding.Default.GetString(ara);  // Convert.ToChar(x);  //System.Text.ASCIIEncoding.UTF32.GetString(deneme, 0, deneme.Length);   //char.ConvertFromUtf32(x);
                ss = ss.Substring(ss.Length - 1) + ss.Substring(0, ss.Length - 1);
            }

            result = s2;
            return result;
        }

        public static string SifreleWinsoft(string metin)
        {
            int a;
            string temp, karakter;

            temp = "";
            karakter = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz1234567890\"é!^+%&/()=?_*-@~ <>|.,:;#${[]}\\xwXWqQ";

            for (int i = 0; i <= metin.Length - 1; i++)
            {
                a = karakter.IndexOf(metin[i], 1);

                if (a < 0)
                    a = 0;

                if ((a < (karakter.Length - 14)) && (a > 0))
                {
                    temp = temp + karakter[a + 15];
                }
                else if ((a > (karakter.Length - 14)) && (a != 0))
                {
                    a = a + 15;
                    temp = temp + karakter[i];
                }

                if (a == 0)
                {
                    temp = temp + metin[i];
                }
            }
            return temp;
        }

        public static string SifreCozWinsoft(string metin)
        {
            int a;
            string temp, karakter;

            temp = "";
            karakter = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz1234567890\"é!^+%&/()=?_*-@~ <>|.,:;#${[]}\\xwXWqQ";

            for (int i = 0; i <= metin.Length - 1; i++)
            {
                a = karakter.IndexOf(metin[i], 1);

                if (a < 0)
                    a = 0;

                if (a > 15)
                {
                    temp = temp + karakter[a - 15];
                }
                else if ((a < 16) && (a != 0))
                {
                    a = a + karakter.Length;
                    temp = temp + karakter[a - 15];
                }

                if (a == 0)
                {
                    temp = temp + metin[i];
                }
            }

            return temp;
        }
    }
}