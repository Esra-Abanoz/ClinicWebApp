using System.ComponentModel;

namespace ClinicWeb.Models.Enums.DuyuruEnum
{
    public enum DuyuruOkunduOkunmadiEnum
    {
        [Description("Okundu")]
        Okundu = 0,
        [Description("Okunmadı")]
        Okunmadı = 1,
        [Description("Hepsi")]
        Hepsi = 2,
    }
}
