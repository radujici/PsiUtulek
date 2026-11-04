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

      
    }
}
