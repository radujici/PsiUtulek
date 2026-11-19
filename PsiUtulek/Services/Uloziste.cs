using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PsiUtulek
{
    internal class Uloziste
    {
        private readonly string cesta;

        public Uloziste(string cesta)
        {
            this.cesta = Path.GetFullPath(cesta);
        }

        public bool Nacist(out List<Zvire> zvirata, out long dalsiId)
        {
            XDocument dokument;
            try
            {
                dokument = XDocument.Load(cesta);
            }
            catch (FileNotFoundException)
            {
                zvirata = new List<Zvire>();
                dalsiId = 1;
                return false;
            }

            XElement koren = dokument.Root;
            if (koren == null || koren.Name != "Zvirata")
            {
                throw new FormatException("Soubor nemá správný formát.");
            }

            zvirata = koren.Elements("Zvire").Select(NacistZvire).ToList();
            int nejvyssiId = zvirata.Select(zvire => zvire.Id).DefaultIfEmpty().Max();
            if (zvirata.Select(zvire => zvire.Id).Distinct().Count() != zvirata.Count ||
                !long.TryParse((string)koren.Attribute("DalsiId"), NumberStyles.None,
                    CultureInfo.InvariantCulture, out dalsiId) ||
                dalsiId <= nejvyssiId || dalsiId > (long)int.MaxValue + 1)
            {
                throw new FormatException("Soubor obsahuje neplatná ID.");
            }

            return true;
        }

        public void Ulozit(List<Zvire> zvirata, long dalsiId)
        {
            var dokument = new XDocument(new XElement("Zvirata",
                new XAttribute("DalsiId", dalsiId), zvirata.Select(zvire =>
                    new XElement("Zvire", new XAttribute("Id", zvire.Id),
                        new XElement("Jmeno", zvire.Jmeno),
                        new XElement("Druh", zvire.Druh),
                        new XElement("Vek", zvire.Vek),
                        new XElement("Pohlavi", zvire.Pohlavi),
                        new XElement("DatumPrijmu", zvire.DatumPrijmu),
                        new XElement("ZdravotniStav", zvire.ZdravotniStav),
                        new XElement("Poznamka", zvire.Poznamka),
                        new XElement("Adoptovano", zvire.Adoptovano),
                        new XElement("DatumAdopce", zvire.DatumAdopce)))));

            string docasnaCesta = cesta + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try
            {
                dokument.Save(docasnaCesta);
                if (File.Exists(cesta))
                {
                    File.Replace(docasnaCesta, cesta, null);
                }
                else
                {
                    File.Move(docasnaCesta, cesta);
                }
            }
            finally
            {
                File.Delete(docasnaCesta);
            }
        }

        private static Zvire NacistZvire(XElement prvek)
        {
            int id = (int)prvek.Attribute("Id");
            int vek = (int)prvek.Element("Vek");
            string jmeno = (string)prvek.Element("Jmeno");
            string druh = (string)prvek.Element("Druh");
            string pohlavi = (string)prvek.Element("Pohlavi");
            bool adoptovano = (bool)prvek.Element("Adoptovano");
            string datumAdopce = (string)prvek.Element("DatumAdopce") ?? "";
            if (id < 1 || vek < 0 || string.IsNullOrWhiteSpace(jmeno) ||
                string.IsNullOrWhiteSpace(pohlavi) || (druh != "Pes" && druh != "Kočka" && druh != "Jiné") ||
                adoptovano == string.IsNullOrWhiteSpace(datumAdopce))
            {
                throw new FormatException("Soubor obsahuje neplatné údaje o zvířeti.");
            }

            return new Zvire(id, jmeno, druh, vek, pohlavi, (string)prvek.Element("DatumPrijmu") ?? "",
                (string)prvek.Element("ZdravotniStav") ?? "", (string)prvek.Element("Poznamka") ?? "")
            {
                Adoptovano = adoptovano,
                DatumAdopce = datumAdopce
            };
        }
    }
}
