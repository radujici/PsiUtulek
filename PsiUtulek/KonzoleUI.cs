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
                Console.WriteLine("Zadej normální číslo od nuly nahoru."); 
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
                Console.WriteLine("Nezadal jsi číslo."); 
            } 
        } 
 
        public void ZobrazStatistiky(List<Zvire> zvirata) 
        { 
            if (zvirata.Count == 0) 
            { 
                Console.WriteLine("Nejsou tu žádná zvířata."); 
                return; 
            } 
 
            int psi = 0; 
            int kocky = 0; 
            int jine = 0; 
            int adoptovano = 0; 
            int celkovyVek = 0; 
 
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
 
                celkovyVek += z.Vek; 
            } 
 
            double prumer = (double)celkovyVek / zvirata.Count; 
 
            Console.WriteLine("Psi: " + psi); 
            Console.WriteLine("Kočky: " + kocky); 
            Console.WriteLine("Ostatní: " + jine); 
            Console.WriteLine("Průměrný věk: " + prumer); 
            Console.WriteLine("Adoptovaných zvířat: " + adoptovano); 
        }                    
    }
}
