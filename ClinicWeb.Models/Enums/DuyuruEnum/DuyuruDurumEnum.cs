using System.ComponentModel;

namespace ClinicWeb.Models.Enums.DuyuruEnum
{
    public enum DuyuruDurumEnum
    {
        [Description("İptal")]
        Iptal = 0,
        [Description("Arşiv")]
        Arsiv = 1,
        [Description("Yayında")]
        Yayinda = 2,
    }
}
