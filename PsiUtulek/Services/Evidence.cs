using System.Collections.Generic;

namespace PsiUtulek
{
    internal class Evidence
    {
        public List<Zvire> SeznamZvirat { get; set; }
        private int dalsiId = 1;

        public Evidence()
        {
            SeznamZvirat = new List<Zvire>();
            PridatZvire("Alík", "Pes", 3, "Samec", "01.01.2025", "Zdravý", "Hravý");
            PridatZvire("Micka", "Kočka", 2, "Samice", "10.02.2025", "Očkovaná", "Klidná");
            PridatZvire("Blesk", "Jiné", 1, "Samec", "15.03.2025", "V pořádku", "Křeček");
        }

        public void PridatZvire(string jmeno, string druh, int vek, string pohlavi, string datumPrijmu, string zdravotniStav, string poznamka)
        {
            Zvire z = new Zvire(dalsiId, jmeno, druh, vek, pohlavi, datumPrijmu, zdravotniStav, poznamka);
            SeznamZvirat.Add(z);
            dalsiId++;
        }

        public Zvire NajdiPodleId(int id)
        {
            foreach (Zvire z in SeznamZvirat)
            {
                if (z.Id == id)
                {
                    return z;
                }
            }
            return null;
        }

        public List<Zvire> FiltrovatPodleDruhu(string druh)
        {
            List<Zvire> vysledek = new List<Zvire>();
            foreach (Zvire z in SeznamZvirat)
            {
                if (z.Druh == druh)
                {
                    vysledek.Add(z);
                }
            }
            return vysledek;
        }

        public List<Zvire> FiltrovatPodleJmena(string jmeno)
        {
            List<Zvire> vysledek = new List<Zvire>();
            foreach (Zvire z in SeznamZvirat)
            {
                if (z.Jmeno.IndexOf(jmeno, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    vysledek.Add(z);
                }
            }
            return vysledek;
        }

        public List<Zvire> FiltrovatPodleVeku(int vek, bool vetsiNeboRovno)
        {
            List<Zvire> vysledek = new List<Zvire>();
            foreach (Zvire z in SeznamZvirat)
            {
                if (vetsiNeboRovno && z.Vek >= vek)
                {
                    vysledek.Add(z);
                }
                else if (!vetsiNeboRovno && z.Vek <= vek)
                {
                    vysledek.Add(z);
                }
            }
            return vysledek;
        }
        public void OznacitAdopci(Zvire zvire, bool adoptovano, string datum)
        {
            zvire.Adoptovano = adoptovano;
            zvire.DatumAdopce = datum;
        }
    }
}
