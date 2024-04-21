using System.ComponentModel;

namespace ClinicWeb.Models.Enums.MuhasebeEnum
{
    public enum TeminatTuruEnum
    {
        [Description("Secilmedi")]
        S = 0,
        [Description("Mektup")]
        Mektup = 1,
        [Description("Nakit Kesin Teminat")]
        NakitKesinTeminat = 2,
        [Description("Nakit Geçici Teminat")]
        NakitGeciciTeminat = 3,
     
    }
}
