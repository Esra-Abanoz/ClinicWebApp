using System.ComponentModel;

namespace ClinicWeb.Models.Enums.DuyuruEnum
{
    public  enum DuyuruTuruEnum
    {
        [Description("Haber")]
        Haber = 0,
        [Description("Duyuru")]
        Duyuru = 1, 
        [Description("Uyari")]
        Uyari = 2,
        [Description("Hepsi")]
        Hepsi = 3,

    }
}
