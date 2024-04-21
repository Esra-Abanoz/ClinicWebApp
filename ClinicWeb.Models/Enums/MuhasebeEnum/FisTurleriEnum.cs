using System.ComponentModel;

namespace ClinicWeb.Models.Enums.MuhasebeEnum
{
    public enum FisTurleriEnum
    {
      //  [Description("Tanımsız")]
      //  Tanimsiz = 0,
        [Description("Muhasebe İşlem Fişi")]
        MuhasebeIslemFisi = 1,
        [Description("Ödeme Emri Belgesi")]
        OdemeEmriBelgesi = 2,
        [Description("Açılış Fişi")]
        AcilisFisi= 3,
        [Description("Kapanış Fişi")]
        KapanisFisi = 4
    }
}
