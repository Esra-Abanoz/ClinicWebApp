using System.ComponentModel;

namespace ClinicWeb.Models.Enums.FaturaAbone
{
    public enum AboneTuru
    {
        [Description("Sabit Telefon")]
        SabitTelefon = 0,

        [Description("ADSL")]
        ADSL = 1,

        [Description("Cep Telefonu")]
        CepTelefonu = 2,

        [Description("Su")]
        Su = 3,

        [Description("Elektirik")]
        Elektrik = 4, 

        [Description("Doğal Gaz")]
        DogalGaz = 5,
    }
}
