using System.ComponentModel;

namespace ClinicWeb.Models.Enums
{
    public enum YetkiEnum
    {
        [Description("KULLANICI TANIMLARI")]
        KullaniciTanimlari = 1,
        [Description("HASTANE TANIMLARI")]
        HastaneTanimlari = 2,
        [Description("ROL TANIMLARI")]
        RolTanimlari = 3,
        [Description("Görev  TANIMLARI")]
        GorevTanimlari = 4,
        [Description("BANKA TANIMLARI")]
        BankaTanimlari = 5,
        [Description("ŞUBE TANIMLARI")]
        SubeTanimlari = 6,
        [Description("FİRMA TANIMLARI")]
        FirmaTanimlari = 7,
        [Description("STOK TANIMLARI")]
        StokTanimlari = 8,
        [Description("KOD TANIMLARI")]
        KodTanimlari = 9,
        [Description("MUHASEBE KURUM TANIMLARI")]
        MuhasebeKurumTanimlari = 10,
        [Description("İMZA KİŞİ TANIMLARI")]
        ImzaKisiTanimlari = 11,
        [Description("HESAP TANIMLARI")]
        HesapTanimlari = 12,
        [Description("BÜTÇE TANIMLARI")]
        ButceTanimlari = 13,
        [Description("BÜTÇE HESAP EŞLEŞTİRME")]
        ButceHesapEslestirme = 14,
        [Description("FİŞ GRUBU TANIMLARI")]
        FisGrubuTanimlari = 15,
        [Description("İCRA TANIMLARI")]
        IcraTanimlari = 16,
        [Description("TEMİNAT TANIMLARI")]
        TeminatTanimlari = 17,
        [Description("TESLİM TUTANAĞI TANIMLARI")]
        TeslimTutanagiTanimlari = 18,
        [Description("MİZAN RAPORU")]
        MizanRaporu = 19,
        [Description("YEVMİYE DEFTERİ RAPORU")]
        YevmiyeDefteriRaporu = 20,
        [Description("YARDIMCI DEFTER RAPORU")]
        YardimciDefterRaporu = 21,
        [Description("FİRMA KESİNTİ RAPORU")]
        FirmaKesintiRaporu = 22,
        [Description("FİRMA BORÇ RAPORU")]
        FirmaBorcRaporu = 23,
        [Description("BÜTÇE TAKİP TUTAR RAPORU")]
        ButceTakipTutarRaporu = 24,
        [Description("MUHASEBE İŞLEM FİŞİ TANIMLAMA")]
        MuhasebeIslemFisiTanimlama = 25,
        [Description("DUYURU TANIMLAMA")]
        DuyuruTanimlama = 26,
        [Description("AVANS TAKİP RAPORU")]
        AvansTakipRaporu = 27,
        [Description("MUHASEBE GİDER RAPORU")]
        MuhasebeGiderRaporu = 28,
        [Description("MUAVİN DEFTER RAPORU")]
        MuavinDefterRaporu = 29,
        [Description("BİLANÇO RAPORU")]
        BilancoRaporu = 30,
        [Description("CARİ KART RAPORU")]
        CariKartRaporu = 31,
        [Description("API CONNECT TANIMLARI")]
        ApiConnectTanimlari = 32,
        [Description("AYRINTILI MİZAN RAPORU")]
        AyrintiliMizanRaporu = 33,
        [Description("DAMGA VERGİSİ RAPORU")]
        DamgaVergisiRaporu = 34,
        [Description("DEFTERİ KEBİR RAPORU")]
        DefteriKebirRaporu = 35,

        [Description("ABONE TANIMLAMA")]
        AboneTanimlari = 36,
        [Description("FATURA DONEMLERI TANIMLAMA")]
        FaturaDonemleriTanimlama = 37,
        [Description("FATURA TANIM TAKİP")]
        FaturaTanimTakip = 38,
        [Description("STOK  AMBAR TANIMLARI")]
        StokAmbarTanimlari = 39,
        [Description("STOK  GRUP TANIMLARI")]
        StokGrupTanimlari = 40,
        [Description("SUT KODLARI TANIMLARI")]
        SutKodlariTanimlari = 41,
        [Description("BİRİM TANIMLARI")]
        BirimTanimlari = 42,
        [Description("KULLANICI BÖLÜM TANIMLARI")]
        KullaniciBolumTanimlari = 43,
        [Description("SATIN ALMA TALEP")]
        SatinAlmaTalep = 43,
        [Description("İHTİYAÇ TESPİT")]
        IhtiyacTespit = 44,
        [Description("ŞARTNAME DOSYA")]
        SartnameDosya = 45,
        [Description("SATINALMA AKTİF GOREVLİ HAVUZU")]
        SatinAlmaAktifGorevliHavuzu = 45,
        [Description("VARSAYILAN DOSYA GÖREVLİSİ TANILAMA")]
        VarsayilanDosyaGorevlisiTanimlama = 46,
        [Description("VARSAYILAN KOMİSYON GÖREVLİSİ TANIMLAMA")]
        VarsayilanKomisyonGorevlisiTanimlama = 47,
        [Description("EŞİK DEĞER PARASAL LİMİT TANIMLAMA")]
        EsikDegerParasalLimitTanimlama = 48,
        [Description("SATIN ALMA VARSAYILAN DEĞERLERİ TANIMLAMA")]
        SatinAlmaVarsayilanDegerTanimlama = 49,
        [Description("SATIN ALMA ÖN TANIMLAMA")]
        SatinAlmaOnTanim = 50,
        [Description("TALEP KARŞILAMA İŞ TAKİP")]
        TalepKarsilamaIsTakip = 51, 
        [Description("KARŞILANMAYAN TALEP LİSTESİ")]
        KarsilanmayanTalepListesi = 52,
        [Description("TALEP DEĞERLENDİRME LİSTESİ")]
        TalepDegerlendirmeListesi = 53,
        [Description("TALEP TOPLAMA LİSTESİ")]
        TalepToplamaListesi = 54,
        
        [Description("STOK ÇIKIŞ SARF MALİYET RAPORU")]
        StokCikisSarfMaliyeTakipRaporu = 55, 
        [Description("STOK ÇIKIŞ RAPORU")]
        StokCikisTakipRaporu = 56, 
        [Description("STOK MALİYET TAKİP RAPORU")]
        StokMaliyetTakipRaporu = 57,
        [Description("DOĞRUDAN TEMİN LİSTESİ")]
        DogrudanTeminListesi = 58, 
        [Description("İHALE DOSYA LİSTESİ")]
        IhaleDosyaListesi = 59, 
        [Description("ŞİPARİŞ LİSTESİ")]
        SiparisListesi = 60,
        [Description("YAKLAŞIK MALİYET LİSTESİ")]
        YaklasikMaliyetListesi = 60,
        [Description("MUHASEBE İŞLEM FATURA")]
        MuhasebeIslemFatura = 61,
        [Description("MUHASEBE İŞLEM BÜTÇE LİSTESİ")]
        MuhasebeIslemButceListesi= 62,

        [Description("MKYS İŞLEMLERİ")]
        MkysIslemleri = 63,
        [Description("FATURA EDİLMİŞ TESLİMATLAR LİSTESİ")]
        FaturaEdilmisTeslimatlarListesi = 64,
        [Description("ŞARTNAME SÖZLEŞME İLAN TANIMLARI")]
        SartnameSozlesmeIlanTanimlari = 65,
        [Description("YASAKLI FİRMA SORGULAMA")]
        YasakliFirmaSorgulama = 66,
        [Description("SATINALMA İHALE BELGE İŞLEMLERİ")]
        SatinalmaIhaleBelgeIslemleri = 67,
    }
}
