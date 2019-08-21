using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;

namespace DaeGirisCikisMesajlari
{
    public class GirişÇıkışMesajlarıYapılandırma : IRocketPluginConfiguration
    {
        public bool OyuncuSayısıAktif { get; set; }
        public ushort OyuncuSayısıEfektIdsi { get; set; }

        public bool GirişMesajlarıAktif { get; set; }
        public string GirişMesajıRengi { get; set; }
        public ushort GirişEfektiIdsi { get; set; }

        public bool ÇıkışMesajlarıAktif { get; set; }
        public string ÇıkışMesajıRengi { get; set; }
        public ushort ÇıkışEfektiIdsi { get; set; }

        public string MevcutOyuncuSayısıRengi { get; set; }
        public string MaksimumOyuncuSayısıRengi { get; set; }
        
        public bool ÖzelEfektlerAktif { get; set; }
        [XmlArrayItem("EfektIdsi")]
        public List<ushort> EfektIdleri { get; set; } = new List<ushort>();

        public void LoadDefaults()
        {
            OyuncuSayısıAktif = true;
            OyuncuSayısıEfektIdsi = 15964;

            GirişMesajlarıAktif = true;
            GirişMesajıRengi = "32FF32";
            GirişEfektiIdsi = 15963;

            ÇıkışMesajlarıAktif = true;
            ÇıkışMesajıRengi = "FF3232";
            ÇıkışEfektiIdsi = 15963;

            MevcutOyuncuSayısıRengi = "FFC800";
            MaksimumOyuncuSayısıRengi = "64FF00";

            ÖzelEfektlerAktif = false;
            EfektIdleri = new List<ushort>
            {
                0,
                45
            };
        }
    }
}