using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsiUtulek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Evidence evidence = new Evidence();
            KonzoleUI ui = new KonzoleUI();
            while (true)
            {
                ui.ZobrazMenu();
                string volba = Console.ReadLine();
                if (volba == "1")
                {
                    string jmeno = ui.NactiText("Jméno: ", true);
                    string druh = ui.NactiText("Druh (pes/kočka/jiné): ", true);
                    int vek = ui.NactiVek();
                    string pohlavi = ui.NactiText("Pohlaví: ", true);
                    string prijem = ui.NactiText("Datum příjmu: ", false);
                    string stav = ui.NactiText("Zdravotní stav: ", false);
                    string poznamka = ui.NactiText("Poznámka: ", false);
                    evidence.PridatZvire(jmeno, druh, vek, pohlavi, prijem, stav, poznamka);
                    Console.WriteLine("Zvíře přidáno.");
                }
                else if (volba == "2")
                {
                    ui.VypisTabulku(evidence.SeznamZvirat);
                }
                else if (volba == "3")
                {
                    Console.WriteLine("1) Podle druhu");
                    Console.WriteLine("2) Podle jména");
                    Console.WriteLine("3) Podle věku");
                    Console.Write("Vyber možnost: ");
                    string filtr = Console.ReadLine();
                    if (filtr == "1")
                    {
                        string druh = ui.NactiText("Zadej druh: ", true);
                        ui.VypisTabulku(evidence.FiltrovatPodleDruhu(druh));
                    }
                    else if (filtr == "2")
                    {
                        string jmeno = ui.NactiText("Zadej jméno: ", true);
                        ui.VypisTabulku(evidence.FiltrovatPodleJmena(jmeno));
                    }
                    else if (filtr == "3")
                    {
                        int vek = ui.NactiVek();
                        Console.Write("1 pro >= nebo 2 pro <= : ");
                        string volbaVeku = Console.ReadLine();
                        bool vetsi = volbaVeku == "1";
                        ui.VypisTabulku(evidence.FiltrovatPodleVeku(vek, vetsi));
                    }
                }
                else if (volba == "4")
                {
                    int id = ui.NactiCislo("Zadej ID zvířete: ");
                    string datum = ui.NactiText("Zadej datum adopce: ", true);
                    if (evidence.OznacitAdopci(id, datum))
                    {
                        Console.WriteLine("Adoptováno.");
                    }
                    else
                    {
                        Console.WriteLine("Zvíře s tímto ID neexistuje.");
                    }
                }
                else if (volba == "5")
                {
                    ui.ZobrazStatistiky(evidence.SeznamZvirat);
                }
                else if (volba == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Špatná volba.");
                }   
            }
        }
    }
}
