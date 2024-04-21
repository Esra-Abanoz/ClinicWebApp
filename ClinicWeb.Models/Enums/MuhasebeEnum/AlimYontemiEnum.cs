using System.ComponentModel;

namespace ClinicWeb.Models.Enums.MuhasebeEnum
{
    public  enum AlimYontemiEnum
    {
       // [Description("Tanımsız")]
      //  Tanimsiz = 0,

        [Description("Madde 3-h")]
        Madde3h = 1,

        [Description("Madde 3-e (Kamuya ait kurum ve kuruluşlar")]
        Madde3e = 2,

        [Description("Madde 19 (Açık ihale usulu)")] 
        Madde19 = 3,

        [Description("Madde 20 (Belli istekler")]
        Madde20 = 4,

        [Description("Madde 21-b (Pazarlık usulü) ")]
        Madde21b = 5,

        [Description("Madde 21-f (Pazarlık usulu)")]
        Madde21f = 6,

        [Description("Madde 22-a (Doğrudan temin)")]
        Madde22a = 7,

        [Description("Madde 22-b (Doğrudan temin)")]
        Madde22b = 8,

        [Description("Madde 22-c (Doğrudan temin)")]
        Madde22c = 9, 

        [Description("Madde 22-d (Doğrudan temin)")]
        Madde22d = 10, 

        [Description("Madde 22-f (Doğrudan temin)")]
        Madde22f = 11,

        [Description("Çerçeve Sözleşme ile alım (Açık)")]
        CerceveSozlesmeileAlimAcik = 12,

        [Description("Çerçeve Sözleşme ile alım (Belli istekler)")]
        CerceveSozlesmeileAlimBelliIstekler = 13,

        [Description("195-Diğer iş avansları")] 
        DigerIsAvanslari195 = 14
  
    }
}
