using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("Nic tu není."); 
                return; 
            } 
 
            Console.WriteLine("ID | Jméno | Druh | Věk | Pohlaví | Příjem | Adoptováno | Datum adopce"); 
            foreach (Zvire z in zvirata) 
            { 
                string adopt = z.Adoptovano ? "Ano (" + z.DatumAdopce + ")" : "Ne"; 
                Console.WriteLine(z.Id + " | " + z.Jmeno + " | " + z.Druh + " | " + z.Vek + " | " + z.Pohlavi + " | " + z.DatumPrijmu + " | " + adopt); 
            } 
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
                Console.WriteLine("Musíš něco zadat."); 
            } 
        }

                              
    }
}
