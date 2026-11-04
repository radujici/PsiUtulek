using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsiUtulek
{
    internal class Evidence
    {
        public List<Zvire> SeznamZvirat { get; set; } = new List<Zvire>();
        private int dalsiId = 1;

        public Evidence()
        {
            PridatZvire("Alík", "Pes", 3, "Samec", "01.01.2025", "Zdravý", "Hravý");
            PridatZvire("Micka", "Kočka", 2, "Samice", "10.02.2025", "Očkovaná", "Klidná");
            PridatZvire("Blesk", "Jiné", 1, "Samec", "15.03.2025", "V pořádku", "Křeček");
        }

        public void PridatZvire(string jmeno, string druh, int vek, string pohlavi, string datumPrijmu, string zdravotniStav, string poznamka)
        {
            Zvire z = new Zvire(dalsiId, jmeno, druh, vek, pohlavi, datumPrijmu, zdravotniStav, poznamka);
            SeznamZvirat.Add(z);
            dalsiId++;
        }
    }
}
