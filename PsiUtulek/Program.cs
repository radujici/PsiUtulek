using System;
using System.IO;

namespace PsiUtulek
{
    internal class Program
    {
        static void Main()
        {
            var evidence = new Evidence();
            var ui = new KonzoleUI();

            try
            {
                while (true)
                {
                    ui.ZobrazMenu();
                    switch (ui.NactiRadek())
                    {
                        case "1":
                            PridatZvire(evidence, ui);
                            break;
                        case "2":
                            ui.VypisTabulku(evidence.SeznamZvirat);
                            break;
                        case "3":
                            Filtrovat(evidence, ui);
                            break;
                        case "4":
                            OznacitAdopci(evidence, ui);
                            break;
                        case "5":
                            ui.ZobrazStatistiky(evidence.SeznamZvirat);
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Špatná volba.");
                            break;
                    }
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine();
            }
        }

        private static void PridatZvire(Evidence evidence, KonzoleUI ui)
        {
            string jmeno = ui.NactiText("Jméno: ", true);
            string druh = ui.NactiDruh();
            int vek = ui.NactiVek();
            string pohlavi = ui.NactiText("Pohlaví: ", true);
            string prijem = ui.NactiDatum("Datum příjmu (DD.MM.RRRR, Enter = prázdné): ", false);
            string stav = ui.NactiText("Zdravotní stav: ", false);
            string poznamka = ui.NactiText("Poznámka: ", false);
            evidence.PridatZvire(jmeno, druh, vek, pohlavi, prijem, stav, poznamka);
            Console.WriteLine("Zvíře přidáno.");
        }

        private static void Filtrovat(Evidence evidence, KonzoleUI ui)
        {
            Console.WriteLine("1) Podle druhu");
            Console.WriteLine("2) Podle jména");
            Console.WriteLine("3) Podle věku");

            switch (ui.NactiVolbu("Vyber možnost: ", "1", "2", "3"))
            {
                case "1":
                    ui.VypisTabulku(evidence.FiltrovatPodleDruhu(ui.NactiDruh()));
                    break;
                case "2":
                    string jmeno = ui.NactiText("Zadej jméno: ", true);
                    ui.VypisTabulku(evidence.FiltrovatPodleJmena(jmeno));
                    break;
                case "3":
                    int vek = ui.NactiVek();
                    string smer = ui.NactiVolbu("1 pro >= nebo 2 pro <=: ", "1", "2");
                    ui.VypisTabulku(evidence.FiltrovatPodleVeku(vek, smer == "1"));
                    break;
            }
        }

        private static void OznacitAdopci(Evidence evidence, KonzoleUI ui)
        {
            int id = ui.NactiId();
            if (evidence.NajdiPodleId(id) == null)
            {
                Console.WriteLine("Zvíře s tímto ID neexistuje.");
                return;
            }

            string volba = ui.NactiVolbu("Adoptováno? 1 = ano, 2 = ne: ", "1", "2");
            bool adoptovano = volba == "1";
            string datum = adoptovano ? ui.NactiDatum("Datum adopce (DD.MM.RRRR): ", true) : "";
            evidence.OznacitAdopci(id, adoptovano, datum);
            Console.WriteLine(adoptovano ? "Adoptováno." : "Adopce zrušena.");
        }
    }
}
