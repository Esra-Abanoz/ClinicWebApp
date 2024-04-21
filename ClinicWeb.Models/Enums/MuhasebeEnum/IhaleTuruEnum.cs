using System.ComponentModel;

namespace ClinicWeb.Models.Enums.MuhasebeEnum
{
    public enum IhaleTuruEnum
    {
        [Description("Sınıfsız")]
        S = 0,
        [Description("Mal")]
        M = 1,
        [Description("Hizmet")]
        H = 2,
        [Description("Yapım")]
        Y = 3,
        [Description("Danışmanlık")]
        D = 4,
    }
}
