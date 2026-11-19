using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PsiUtulek
{
    internal class Evidence
    {
        private readonly Uloziste uloziste;
        private long dalsiId;
        public List<Zvire> SeznamZvirat { get; private set; }

        public Evidence(string cesta)
        {
            uloziste = new Uloziste(cesta);
            List<Zvire> zvirata;
            if (!uloziste.Nacist(out zvirata, out dalsiId))
            {
                zvirata.Add(new Zvire(1, "Alík", "Pes", 3, "Samec", "01.01.2025", "Zdravý", "Hravý"));
                zvirata.Add(new Zvire(2, "Micka", "Kočka", 2, "Samice", "10.02.2025", "Očkovaná", "Klidná"));
                zvirata.Add(new Zvire(3, "Blesk", "Jiné", 1, "Samec", "15.03.2025", "V pořádku", "Křeček"));
                dalsiId = 4;
            }

            SeznamZvirat = zvirata;
        }

        public void PridatZvire(Zvire zvire)
        {
            if (dalsiId > int.MaxValue)
            {
                throw new IOException("Došla dostupná ID.");
            }

            zvire.Id = (int)dalsiId;
            var zvirata = new List<Zvire>(SeznamZvirat) { zvire };
            Ulozit(zvirata, dalsiId + 1);
        }

        public void UpravitZvire(Zvire puvodni, Zvire upravene)
        {
            upravene.Id = puvodni.Id;
            upravene.Adoptovano = puvodni.Adoptovano;
            upravene.DatumAdopce = puvodni.DatumAdopce;
            NahraditZvire(upravene);
        }

        public void SmazatZvire(int id)
        {
            Ulozit(SeznamZvirat.Where(zvire => zvire.Id != id).ToList(), dalsiId);
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

        public void OznacitAdopci(Zvire zvire, bool adoptovano, string datum)
        {
            var upravene = new Zvire(zvire.Id, zvire.Jmeno, zvire.Druh, zvire.Vek, zvire.Pohlavi, zvire.DatumPrijmu,
                zvire.ZdravotniStav, zvire.Poznamka)
            {
                Adoptovano = adoptovano,
                DatumAdopce = adoptovano ? datum : ""
            };
            NahraditZvire(upravene);
        }

        public string ExportovatKartu(Zvire zvire)
        {
            Directory.CreateDirectory("karty");
            string cesta = Path.Combine("karty", "zvire-" + zvire.Id + ".txt");
            File.WriteAllLines(cesta, new[]
            {
                "ID: " + zvire.Id,
                "Jméno: " + zvire.Jmeno,
                "Druh: " + zvire.Druh,
                "Věk: " + zvire.Vek,
                "Pohlaví: " + zvire.Pohlavi,
                "Datum příjmu: " + zvire.DatumPrijmu,
                "Zdravotní stav: " + zvire.ZdravotniStav,
                "Poznámka: " + zvire.Poznamka,
                "Adoptováno: " + (zvire.Adoptovano ? "Ano" : "Ne"),
                "Datum adopce: " + zvire.DatumAdopce
            });
            return cesta;
        }

        private void NahraditZvire(Zvire upravene)
        {
            Ulozit(SeznamZvirat.Select(zvire => zvire.Id == upravene.Id ? upravene : zvire).ToList(), dalsiId);
        }

        private void Ulozit(List<Zvire> zvirata, long nasledujiciId)
        {
            uloziste.Ulozit(zvirata, nasledujiciId);
            SeznamZvirat = zvirata;
            dalsiId = nasledujiciId;
        }
    }
}
