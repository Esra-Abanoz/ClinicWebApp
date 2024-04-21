using System.ComponentModel;

namespace ClinicWeb.Models.Enums
{
    public enum KullaniciTipiEnum
    {
        [Description("Boş")]
        Bos = 0,
        [Description("İl Sağlık")]
        IlSaglik = 1, 
        [Description("Admin")]
        Admin = 2, 
        [Description("Satın Alma")]
        SatinAlma = 3,
        [Description("Tedarikçi Firma")]
        TedarikciFirma = 4,
        [Description("Sağlık Tesisi")]
        SaglikTesisi = 5,
    }
}
