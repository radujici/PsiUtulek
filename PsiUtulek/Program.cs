using System;

namespace PsiUtulek
{
    internal class Program
    {
        static void Main()
        {
            Evidence evidence = new Evidence();
            KonzoleUI ui = new KonzoleUI();
            while (true)
            {
                ui.ZobrazMenu();
                string volba = ui.NactiRadek();
                if (volba == "1")
                {
                    string jmeno = ui.NactiText("Jméno: ", true);
                    string druh = ui.NactiDruh();
                    int vek = ui.NactiVek();
                    string pohlavi = ui.NactiText("Pohlaví: ", true);
                    string prijem = ui.NactiDatum("Datum příjmu (d.m.rrrr, nepovinné): ", false);
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
                    Console.Write("Volba: ");
                    string filtr = ui.NactiRadek();
                    if (filtr == "1")
                    {
                        string druh = ui.NactiDruh();
                        ui.VypisTabulku(evidence.FiltrovatPodleDruhu(druh));
                    }
                    else if (filtr == "2")
                    {
                        string jmeno = ui.NactiText("Jméno: ", true);
                        ui.VypisTabulku(evidence.FiltrovatPodleJmena(jmeno));
                    }
                    else if (filtr == "3")
                    {
                        int vek = ui.NactiVek();
                        Console.Write("1) Nejméně  2) Nejvýše: ");
                        string smer = ui.NactiRadek();
                        if (smer == "1" || smer == "2")
                        {
                            ui.VypisTabulku(evidence.FiltrovatPodleVeku(vek, smer == "1"));
                        }
                        else
                        {
                            Console.WriteLine("Špatná volba.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Špatná volba.");
                    }
                }
                else if (volba == "4")
                {
                    int id = ui.NactiId("ID zvířete: ");
                    Zvire zvire = evidence.NajdiPodleId(id);
                    if (zvire == null)
                    {
                        Console.WriteLine("Zvíře s tímto ID neexistuje.");
                        continue;
                    }

                    Console.Write("Adoptováno (1 ano, 2 ne): ");
                    string adopce = ui.NactiRadek();
                    if (adopce == "1")
                    {
                        string datum = ui.NactiDatum("Datum adopce (d.m.rrrr): ", true);
                        evidence.OznacitAdopci(zvire, true, datum);
                    }
                    else if (adopce == "2")
                    {
                        evidence.OznacitAdopci(zvire, false, "");
                    }
                    else
                    {
                        Console.WriteLine("Špatná volba.");
                    }
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
