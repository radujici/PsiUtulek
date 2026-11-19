# Útulek pro zvířata

Konzolová aplikace v C# pro evidenci psů, koček a ostatních zvířat. Každé zvíře má automatické ID, jméno, druh, věk v letech, pohlaví a volitelné datum příjmu, zdravotní stav a poznámku.

## Co aplikace umí

- Přidat zvíře a vypsat evidenci v tabulce.
- Filtrovat podle druhu, části jména a věku (nejvýše nebo alespoň zadaný počet let).
- Označit nebo zrušit adopci a uložit datum adopce.
- Zobrazit počty podle druhu, průměrný věk a počet adopcí.
- Upravit nebo smazat záznam a exportovat jeho kartu do TXT.

Při prvním spuštění jsou v evidenci tři ukázková zvířata. Po první úspěšné změně aplikace uloží evidenci do `zvirata.xml` a při příštím spuštění ji načte. Karty se ukládají do složky `karty`. Obě cesty jsou vůči pracovní složce aplikace.

## Spuštění

1. Otevři `PsiUtulek.sln` ve Visual Studiu na Windows s podporou .NET Framework 4.7.2.
2. Spusť projekt klávesami **Ctrl+F5**.
3. Vyber číslo v nabídce. Pro ukončení zvol **0**.

Po sestavení lze spustit přímo také `PsiUtulek\bin\Release\PsiUtulek.exe` nebo odpovídající soubor ze složky `Debug`.

## Tým

| Člen | Dosavadní práce podle historie repozitáře |
| --- | --- |
| Vojtěch Veselý | Založení projektu, model zvířete a evidence |
| Filip Šárek | Úpravy konzolového kódu a README |
| Jan Válek | Čtení číselných vstupů a statistiky |
| Martin Zapletal | Konzolové rozhraní, tabulka a vstupy |
| Matěj Zendulka | Hlavní nabídka, výpis a filtry |
