using System.ComponentModel;

namespace ClinicWeb.Models.Enums
{
    public enum SistemParemetreleriEnum
    {
        [Description("Tanımsız")]
        Tanimsiz = 0,
        [Description("Fiziksel Yol")]
        FizikselYol = 1,
        [Description("Session Süre")]
        SessionSure = 2,
        [Description("Combobox Değer")]
        ComboboxDeger = 3
    } 
    public enum SistemParemetleriVarsayilanDegerEnum
    {
        [Description("Evet")]
        Evet = 0,
        [Description("Hayır")]
        Hayir = 1
    }
}
