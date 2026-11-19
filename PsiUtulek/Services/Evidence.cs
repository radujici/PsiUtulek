using System;
using System.Collections.Generic;
using System.Linq;

namespace PsiUtulek
{
    internal class Evidence
    {
        public List<Zvire> SeznamZvirat { get; private set; }
        private int dalsiId = 1;

        public Evidence()
        {
            SeznamZvirat = new List<Zvire>();
            PridatZvire("Alík", "Pes", 3, "Samec", "01.01.2025", "Zdravý", "Hravý");
            PridatZvire("Micka", "Kočka", 2, "Samice", "10.02.2025", "Očkovaná", "Klidná");
            PridatZvire("Blesk", "Jiné", 1, "Samec", "15.03.2025", "V pořádku", "Křeček");
        }

        public void PridatZvire(string jmeno, string druh, int vek, string pohlavi, string datumPrijmu,
            string zdravotniStav, string poznamka)
        {
            SeznamZvirat.Add(new Zvire(dalsiId++, jmeno, druh, vek, pohlavi, datumPrijmu,
                zdravotniStav, poznamka));
        }

        public Zvire NajdiPodleId(int id)
        {
            return SeznamZvirat.Find(zvire => zvire.Id == id);
        }

        public List<Zvire> FiltrovatPodleDruhu(string druh)
        {
            return SeznamZvirat.Where(zvire => zvire.Druh == druh).ToList();
        }

        public List<Zvire> FiltrovatPodleJmena(string jmeno)
        {
            return SeznamZvirat.Where(zvire => zvire.Jmeno.IndexOf(jmeno,
                StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
        }

        public List<Zvire> FiltrovatPodleVeku(int vek, bool vetsiNeboRovno)
        {
            return SeznamZvirat.Where(zvire => vetsiNeboRovno ? zvire.Vek >= vek : zvire.Vek <= vek).ToList();
        }

        public void OznacitAdopci(int id, bool adoptovano, string datum)
        {
            Zvire zvire = NajdiPodleId(id);
            zvire.Adoptovano = adoptovano;
            zvire.DatumAdopce = adoptovano ? datum : "";
        }
    }
}
