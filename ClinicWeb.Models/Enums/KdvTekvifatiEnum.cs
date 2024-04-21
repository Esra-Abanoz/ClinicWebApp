using System.ComponentModel;

namespace ClinicWeb.Models.Enums
{
    public enum KdvTekvifatiEnum
    {
        [Description( "1/1")]
        Birbolubir ,
        [Description("1/2")]
        Birboluiki , 
        [Description("1/3")]
        Birboluuc , 
        [Description("2/3")]
        IkiBoluuc ,
        [Description("1/6")]
        Birbolualtı ,
        [Description("4/5")]
        Dortbolubes ,
        [Description("2/10")]
        Ikiboluon , 
        [Description("5/10")]
        Besboluon , 
        [Description("7/10")]
        Yediboluon ,
        [Description("9/10")]
        Dokuzboluon,
        [Description("3/10")]
        Ucboluon ,
    }
}
