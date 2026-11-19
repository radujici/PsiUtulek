using System;
using System.Collections.Generic;
using System.Globalization;

namespace PsiUtulek
{
    internal class KonzoleUI
    {
        public void ZobrazMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== ÚTULEK PRO ZVÍŘATA =====");
            Console.WriteLine("1) Přidat zvíře");
            Console.WriteLine("2) Vypsat všechna zvířata");
            Console.WriteLine("3) Vyhledat/filtrovat");
            Console.WriteLine("4) Označit adopci");
            Console.WriteLine("0) Konec");
            Console.Write("Volba: ");
        }

        public void VypisTabulku(List<Zvire> zvirata)
        {
            if (zvirata.Count == 0)
            {
                Console.WriteLine("Žádné zvíře.");
                return;
            }

            Console.WriteLine("ID | Jméno | Druh | Věk | Pohlaví | Příjem | Zdravotní stav | Poznámka | Adopce | Datum adopce");
            foreach (Zvire z in zvirata)
            {
                string adopce = z.Adoptovano ? "Ano" : "Ne";
                Console.WriteLine(z.Id + " | " + z.Jmeno + " | " + z.Druh + " | " + z.Vek + " | " + z.Pohlavi +
                    " | " + z.DatumPrijmu + " | " + z.ZdravotniStav + " | " + z.Poznamka + " | " + adopce + " | " + z.DatumAdopce);
            }
        }

        public string NactiRadek()
        {
            string vstup = Console.ReadLine();
            if (vstup == null)
            {
                Environment.Exit(0);
            }
            return vstup.Trim();
        }

        public string NactiText(string text, bool povinne)
        {
            while (true)
            {
                Console.Write(text);
                string vstup = NactiRadek();
                if (!povinne || vstup != "")
                {
                    return vstup;
                }
                Console.WriteLine("Musíš něco zadat.");
            }
        }

        public string NactiDruh()
        {
            while (true)
            {
                Console.Write("Druh (1 pes, 2 kočka, 3 jiné): ");
                string druh = NactiRadek();
                if (druh == "1")
                {
                    return "Pes";
                }
                if (druh == "2")
                {
                    return "Kočka";
                }
                if (druh == "3")
                {
                    return "Jiné";
                }
                Console.WriteLine("Špatná volba.");
            }
        }

        public string NactiDatum(string text, bool povinne)
        {
            while (true)
            {
                string vstup = NactiText(text, povinne);
                if (vstup == "")
                {
                    return "";
                }

                DateTime datum;
                if (DateTime.TryParseExact(vstup, "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datum))
                {
                    return datum.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                Console.WriteLine("Zadej platné datum, třeba 5.2.2025.");
            }
        }

        public int NactiVek()
        {
            while (true)
            {
                Console.Write("Věk v letech: ");
                int vek;
                if (int.TryParse(NactiRadek(), out vek) && vek >= 0)
                {
                    return vek;
                }
                Console.WriteLine("Zadej celé číslo od nuly.");
            }
        }

        public int NactiCislo(string text)
        {
            while (true)
            {
                Console.Write(text);
                int cislo;
                if (int.TryParse(NactiRadek(), out cislo))
                {
                    return cislo;
                }
                Console.WriteLine("Zadej celé číslo.");
            }
        }
    }
}
