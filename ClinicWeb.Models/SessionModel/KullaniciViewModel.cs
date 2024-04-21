namespace ClinicWeb.Models.SessionModel
{
    [Serializable]
    public class KullaniciViewModel
    {
        public long Id { get; set; }
        public string AdSoyad { get; set; }
        public string Sifre { get; set; }
        public string KullaniciAdi { get; set; }
        public string Telefon { get; set; }
        public string EMail { get; set; }
        public string TcKimlik { get; set; }
        public DateTime SonGirisZamani { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int HastaneId { get; set; }
        public int BagliHastaneId { get; set; }
        public bool BagliHastane { get; set; }
        public int ButceTuru { get; set; }
        public int Yil { get; set; }
        public bool Iptal { get; set; }
        public int Admin { get; set; }
        public bool SmsGonder { get; set; }
        public bool MailGonder { get; set; }
        public bool TasinirYetkiliMi { get; set; }
        public string HastaneAdi { get; set; }
        public string BaslangicIp { get; set; }
        public string BitisIp { get; set; }
        public string MkysKullaniciAdi { get; set; }
        public string MkysSifre { get; set; }
        public int KurumKodu { get; set; }
    }
}
