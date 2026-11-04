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
                
            }
        }
    }
}
