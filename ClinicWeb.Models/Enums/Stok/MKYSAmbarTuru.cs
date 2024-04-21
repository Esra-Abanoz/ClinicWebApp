using System.ComponentModel;

namespace ClinicWeb.Models.Enums.Stok
{
    public enum MKYSAmbarTuru
    {
        [Description("Medikal Depo - İlaç")]
        MedikalIlac = 0,
        [Description("Medikal Depo - Cerrahi Alet")]
        MedikalCerrahiAlet = 1,
        [Description("Medikal Depo - Laboratuvar")]
        MedikalLaboratuvar = 2,
        [Description("Medikal Depo - Tibbi Sarf")]
        MedikalTibbiSarf = 3,
        [Description("Biyomedikal Tüketim Depo")]
        BiyomedikalTuketim = 4,
        [Description("Biyomedikal Dayanıklı Taşınır Depo")]
        BiyomedikalDayanikliTasinir = 5,
        [Description("Ayniyat Tüketim Depo")]
        AyniyatTuketim = 6,
        [Description("Ayniyat Dayanıklı Taşınır Depo")]
        AyniyatDayanikliTasinir = 7,
    }
}
