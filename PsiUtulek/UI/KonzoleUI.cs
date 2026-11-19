using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PsiUtulek
{
    internal class KonzoleUI
    {
        private static readonly string[] FormatyData = { "d.M.yyyy", "dd.MM.yyyy", "yyyy-MM-dd" };

        public void ZobrazMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== ÚTULEK PRO ZVÍŘATA =====");
            Console.WriteLine("1) Přidat zvíře");
            Console.WriteLine("2) Vypsat všechna zvířata");
            Console.WriteLine("3) Vyhledat / filtrovat");
            Console.WriteLine("4) Označit adopci");
            Console.WriteLine("5) Statistiky");
            Console.WriteLine("6) Upravit zvíře");
            Console.WriteLine("7) Smazat zvíře");
            Console.WriteLine("8) Exportovat kartu zvířete");
            Console.WriteLine("0) Konec");
            Console.Write("Volba: ");
        }

        public void VypisTabulku(List<Zvire> zvirata)
        {
            if (zvirata.Count == 0)
            {
                Console.WriteLine("Žádná zvířata.");
                return;
            }

            var radky = new List<string[]>
            {
                new[] { "ID", "Jméno", "Druh", "Věk", "Pohlaví", "Příjem", "Zdravotní stav",
                    "Poznámka", "Adoptováno", "Datum adopce" }
            };

            foreach (Zvire zvire in zvirata)
            {
                radky.Add(new[] { zvire.Id.ToString(), zvire.Jmeno, zvire.Druh, zvire.Vek.ToString(),
                    zvire.Pohlavi, zvire.DatumPrijmu, zvire.ZdravotniStav, zvire.Poznamka,
                    zvire.Adoptovano ? "Ano" : "Ne", zvire.DatumAdopce });
            }

            int[] sirky = Enumerable.Range(0, radky[0].Length)
                .Select(sloupec => radky.Max(radek => radek[sloupec].Length)).ToArray();

            foreach (string[] radek in radky)
            {
                Console.WriteLine(string.Join(" | ", radek.Select((text, i) =>
                    i == sirky.Length - 1 ? text : text.PadRight(sirky[i]))));
            }
        }

        public Zvire NactiZvire()
        {
            string jmeno = NactiText("Jméno: ", true);
            string druh = NactiDruh();
            int vek = NactiVek();
            string pohlavi = NactiText("Pohlaví: ", true);
            string prijem = NactiDatum("Datum příjmu (DD.MM.RRRR, Enter = prázdné): ", false);
            string stav = NactiText("Zdravotní stav: ", false);
            string poznamka = NactiText("Poznámka: ", false);
            return new Zvire(0, jmeno, druh, vek, pohlavi, prijem, stav, poznamka);
        }

        public string NactiRadek()
        {
            string vstup = Console.ReadLine();
            if (vstup == null)
            {
                throw new EndOfStreamException();
            }

            return vstup.Trim();
        }

        public string NactiText(string vyzva, bool povinne)
        {
            while (true)
            {
                Console.Write(vyzva);
                string vstup = NactiRadek();
                if (!povinne || vstup.Length > 0)
                {
                    return vstup;
                }

                Console.WriteLine("Musíš něco zadat.");
            }
        }

        public string NactiVolbu(string vyzva, params string[] moznosti)
        {
            while (true)
            {
                Console.Write(vyzva);
                string vstup = NactiRadek();
                if (moznosti.Contains(vstup))
                {
                    return vstup;
                }

                Console.WriteLine("Špatná volba.");
            }
        }

        public string NactiDruh()
        {
            while (true)
            {
                string vstup = NactiText("Druh (pes/kočka/jiné): ", true).ToLowerInvariant();
                switch (vstup)
                {
                    case "pes":
                        return "Pes";
                    case "kočka":
                    case "kocka":
                        return "Kočka";
                    case "jiné":
                    case "jine":
                        return "Jiné";
                    default:
                        Console.WriteLine("Zadej pes, kočka nebo jiné.");
                        break;
                }
            }
        }

        public string NactiDatum(string vyzva, bool povinne)
        {
            while (true)
            {
                string vstup = NactiText(vyzva, povinne);
                if (vstup.Length == 0)
                {
                    return "";
                }

                DateTime datum;
                if (DateTime.TryParseExact(vstup, FormatyData, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out datum))
                {
                    return datum.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                }

                Console.WriteLine("Zadej platné datum ve tvaru DD.MM.RRRR.");
            }
        }

        public int NactiVek()
        {
            return NactiCislo("Zadej věk: ", 0);
        }

        public int NactiId()
        {
            return NactiCislo("Zadej ID zvířete: ", 1);
        }

        private int NactiCislo(string vyzva, int minimum)
        {
            while (true)
            {
                Console.Write(vyzva);
                int cislo;
                if (int.TryParse(NactiRadek(), out cislo) && cislo >= minimum)
                {
                    return cislo;
                }

                Console.WriteLine("Zadej celé číslo alespoň " + minimum + ".");
            }
        }

        public void ZobrazStatistiky(List<Zvire> zvirata)
        {
            if (zvirata.Count == 0)
            {
                Console.WriteLine("Žádná zvířata.");
                return;
            }

            foreach (var skupina in zvirata.GroupBy(zvire => zvire.Druh).OrderBy(skupina => skupina.Key))
            {
                Console.WriteLine(skupina.Key + ": " + skupina.Count());
            }

            Console.WriteLine("Průměrný věk: " + zvirata.Average(zvire => (double)zvire.Vek).ToString("0.##"));
            Console.WriteLine("Adoptovaných zvířat: " + zvirata.Count(zvire => zvire.Adoptovano));
        }
    }
}
