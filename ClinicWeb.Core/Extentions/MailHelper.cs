using System.Net.Mail;

namespace ClinicWeb.Core.Extentions
{
    public class MailHelper
    {
        private string Host { get; set; }
        private int Port { get; set; }
        private string KullaniciSifre { get; set; }
        private string GonderenMail { get; set; }
        private string MailImza { get; set; }
        private bool SSL { get; set; }

        public MailHelper(string Host, int Port,  string Sifre, string GonderenMail, string MailImza, bool SSL)
        {
            this.Host = Host;
            this.Port = Port;
            this.KullaniciSifre = Sifre;
            this.GonderenMail = GonderenMail;
            this.MailImza = MailImza;
            this.SSL = SSL;
        }


        public void Gonder(List<string> adresler, string MailKonu, string MailIcerik)
        {
            var mail = new SmtpClient
            {
                Host = Host,
                Port = Port,
                Credentials = new System.Net.NetworkCredential(GonderenMail, KullaniciSifre),
                EnableSsl = SSL
            };
            var mesaj = new MailMessage
            {
                IsBodyHtml = true, 
                Priority = MailPriority.High,
                From = new MailAddress(GonderenMail)
            };
            foreach (var adres in adresler)
            {
                try
                {
                    mesaj.To.Clear();
                    mesaj.To.Add(adres);
                    mesaj.Subject = MailKonu;
                    mesaj.Body = MailIcerik + "<br><br><br>" + MailImza;
                    mail.Send(mesaj);
                }
                catch(Exception e)
                {
                    // ignored
                }
            }
           
        }

        public void Gonder(List<string> adresler, string MailKonu, string MailIcerik, List<Attachment> dosyalar)
        {
            var mail = new SmtpClient
            {
                Host = Host,
                Port = Port,
                Credentials = new System.Net.NetworkCredential(GonderenMail, KullaniciSifre),
                EnableSsl = SSL
            };
            MailMessage mesaj = new MailMessage();
            mesaj.IsBodyHtml = true;
            mesaj.Priority = MailPriority.High;
            mesaj.From = new MailAddress(GonderenMail);
            foreach (string adres in adresler)
            {
                try
                {
                    mesaj.To.Clear();
                    mesaj.To.Add(adres);
                    mesaj.Subject = MailKonu;
                    mesaj.Body = MailIcerik + "<br><br><br>" + MailImza;
                    mail.Send(mesaj);
                }
                catch
                {
                    // ignored
                }
                
                if (dosyalar!=null && dosyalar.Any())
                {
                    foreach (Attachment dosya in dosyalar)
                    {
                        mesaj.Attachments.Add(dosya);
                    }

                    mesaj.Subject = MailKonu;
                    mesaj.Body = MailIcerik + "<br><br><br>" + MailImza;
                    mail.Send(mesaj);
                }
            }
        }
    }
}
