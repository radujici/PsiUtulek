using System;
using System.IO;
using System.Text;
using System.Xml;

namespace PsiUtulek
{
    internal class Program
    {
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Evidence evidence = NacistEvidenci();
            if (evidence == null)
            {
                Environment.ExitCode = 1;
                return;
            }

            var ui = new KonzoleUI();
            while (true)
            {
                try
                {
                    ui.ZobrazMenu();
                    if (!ProvedVolbu(evidence, ui))
                    {
                        return;
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine();
                    return;
                }
                catch (IOException chyba)
                {
                    Console.WriteLine("Chyba při práci se souborem: " + chyba.Message);
                }
                catch (UnauthorizedAccessException chyba)
                {
                    Console.WriteLine("Chyba při práci se souborem: " + chyba.Message);
                }
                catch (ArgumentException chyba)
                {
                    Console.WriteLine("Záznam se nepodařilo uložit: " + chyba.Message);
                }
            }
        }

        private static Evidence NacistEvidenci()
        {
            try
            {
                return new Evidence("zvirata.xml");
            }
            catch (Exception chyba)
            {
                if (!(chyba is IOException || chyba is UnauthorizedAccessException ||
                    chyba is XmlException || chyba is FormatException ||
                    chyba is ArgumentException || chyba is OverflowException))
                {
                    throw;
                }

                Console.Error.WriteLine("Evidenci se nepodařilo načíst: " + chyba.Message);
                return null;
            }
        }

        private static bool ProvedVolbu(Evidence evidence, KonzoleUI ui)
        {
            switch (ui.NactiRadek())
            {
                case "1":
                    evidence.PridatZvire(ui.NactiZvire());
                    Console.WriteLine("Zvíře přidáno.");
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
                case "6":
                    UpravitZvire(evidence, ui);
                    break;
                case "7":
                    SmazatZvire(evidence, ui);
                    break;
                case "8":
                    Zvire zvire = NajdiZvire(evidence, ui);
                    if (zvire != null)
                    {
                        Console.WriteLine("Karta uložena: " + evidence.ExportovatKartu(zvire));
                    }
                    break;
                case "0":
                    return false;
                default:
                    Console.WriteLine("Špatná volba.");
                    break;
            }

            return true;
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

        private static Zvire NajdiZvire(Evidence evidence, KonzoleUI ui)
        {
            Zvire zvire = evidence.NajdiPodleId(ui.NactiId());
            if (zvire == null)
            {
                Console.WriteLine("Zvíře s tímto ID neexistuje.");
            }

            return zvire;
        }

        private static void OznacitAdopci(Evidence evidence, KonzoleUI ui)
        {
            Zvire zvire = NajdiZvire(evidence, ui);
            if (zvire == null)
            {
                return;
            }

            bool adoptovano = ui.NactiVolbu("Adoptováno? 1 = ano, 2 = ne: ", "1", "2") == "1";
            string datum = adoptovano ? ui.NactiDatum("Datum adopce (DD.MM.RRRR): ", true) : "";
            evidence.OznacitAdopci(zvire, adoptovano, datum);
            Console.WriteLine(adoptovano ? "Adoptováno." : "Adopce zrušena.");
        }

        private static void UpravitZvire(Evidence evidence, KonzoleUI ui)
        {
            Zvire zvire = NajdiZvire(evidence, ui);
            if (zvire != null)
            {
                Console.WriteLine("Zadej nové údaje zvířete.");
                evidence.UpravitZvire(zvire, ui.NactiZvire());
                Console.WriteLine("Zvíře upraveno.");
            }
        }

        private static void SmazatZvire(Evidence evidence, KonzoleUI ui)
        {
            Zvire zvire = NajdiZvire(evidence, ui);
            if (zvire != null && ui.NactiVolbu("Smazat? 1 = ano, 2 = ne: ", "1", "2") == "1")
            {
                evidence.SmazatZvire(zvire.Id);
                Console.WriteLine("Zvíře smazáno.");
            }
        }
    }
}
