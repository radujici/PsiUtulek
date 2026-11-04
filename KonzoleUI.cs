using System;
using System.Collections.Generic;
using Utulek.Model;

namespace Utulek.UI
{
    public class KonzoleUI
    {
        public void ZobrazMenu()
        {
            Console.WriteLine("\n===== ÚTULEK PRO ZVÍŘATA =====");
            Console.WriteLine("1) Přidat zvíře");
            Console.WriteLine("2) Vypsat všechna zvířata");
            Console.WriteLine("3) Vyhledat/ filtrovat");
            Console.WriteLine("4) Označit adopci");
            Console.WriteLine("5) Statistiky (volitelné)");
            Console.WriteLine("0) Konec");
            Console.Write("Volba: ");
        }

        public void VypisTabulku(List<Zvire> zvirata)
        {
            if (zvirata.Count == 0)
            {
                Console.WriteLine("Žádná zvířata nebyla nalezena.");
                return;
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine(string.Format("{0,-4} | {1,-10} | {2,-8} | {3,-4} | {4,-8} | {5,-12} | {6,-10} | {7,-12}",
                "ID", "Jméno", "Druh", "Věk", "Pohlaví", "Příjem", "Adopce", "Datum adopce"));
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");

            foreach (Zvire z in zvirata)
            {
                string adopt = z.Adoptovano ? "Ano" : "Ne";
                Console.WriteLine(string.Format("{0,-4} | {1,-10} | {2,-8} | {3,-4} | {4,-8} | {5,-12} | {6,-10} | {7,-12}",
                    z.Id, z.Jmeno, z.Druh, z.Vek, z.Pohlavi, z.DatumPrijmu, adopt, z.DatumAdopce));
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        }

        public string NactiText(string text, bool povinne)
        {
            while (true)
            {
                Console.Write(text);
                string vstup = Console.ReadLine();
                if (!povinne || !string.IsNullOrWhiteSpace(vstup))
                {
                    return vstup;
                }
                Console.WriteLine("Tato hodnota nesmí být prázdná!");
            }
        }

        public int NactiVek()
        {
            while (true)
            {
                Console.Write("Zadej věk: ");
                string vstup = Console.ReadLine();
                if (int.TryParse(vstup, out int vek) && vek >= 0)
                {
                    return vek;
                }
                Console.WriteLine("Věk musí být celé číslo >= 0!");
            }
        }
        public int NactiCislo(string text)
        {
            while (true)
            {
                Console.Write(text);
                string vstup = Console.ReadLine();
                if (int.TryParse(vstup, out int cislo))
                {
                    return cislo;
                }
                Console.WriteLine("Zadej platné číslo!");
            }
        }
        public void ZobrazStatistiky(List<Zvire> zvirata)
        {
            if (zvirata.Count == 0)
            {
                Console.WriteLine("Žádná data pro statistiky.");
                return;
            }

            int psi = 0;
            int kocky = 0;
            int jine = 0;
            int adoptovano = 0;
            int soucetVeku = 0;

            foreach (Zvire z in zvirata)
            {
                if (z.Druh.ToLower() == "pes")
                {
                    psi++;
                }
                else if (z.Druh.ToLower() == "kočka" || z.Druh.ToLower() == "kocka")
                {
                    kocky++;
                }
                else
                {
                    jine++;
                }

                if (z.Adoptovano)
                {
                    adoptovano++;
                }

                soucetVeku += z.Vek;
            }

            double prumer = (double)soucetVeku / zvirata.Count;

            Console.WriteLine("\n--- STATISTIKY ---");
            Console.WriteLine("Počet psů: " + psi);
            Console.WriteLine("Počet koček: " + kocky);
            Console.WriteLine("Počet jiných zvířat: " + jine);
            Console.WriteLine("Průměrný věk: " + Math.Round(prumer, 1) + " let");
            Console.WriteLine("Počet adoptovaných: " + adoptovano);
        }
    }
}
