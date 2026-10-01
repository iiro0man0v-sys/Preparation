namespace Preparation;

class Program
{
    static void Main(string[] args)
    {
        // =========================================================================
        // 1. UKÁZKA TŘÍDY MobilniTarif
        // =========================================================================
        Console.WriteLine("==================================================");
        Console.WriteLine("           DEMONSTRACE: MobilniTarif              ");
        Console.WriteLine("==================================================");

        // Vytvoření nového tarifu s měsíčním limitem 10 GB (výchozí stav je Aktivní)
        MobilniTarif tarif = new MobilniTarif(10.0m);
        Console.WriteLine($"Vytvořen tarif: Limit = {tarif.MesicniLimitGB} GB, Využito = {tarif.VyuzitaDataGB} GB, Stav = {tarif.Stav}");
        Console.WriteLine($"Využito v MB: {tarif.VyuzitaDataMB} MB");

        // 1.1 Použití metody PridejSpotrebovanaData
        Console.WriteLine("\n--- Přidání spotřebovaných dat (2.5 GB) ---");
        tarif.PridejSpotrebovanaData(2.5m);
        Console.WriteLine($"Aktuálně využito: {tarif.VyuzitaDataGB} GB ({tarif.VyuzitaDataMB} MB)");

        // 1.2 Použití vlastnosti VyuzitaDataMB (čtení i zápis)
        Console.WriteLine("\n--- Nastavení využitých dat přes vlastnost v MB (5120 MB = 5 GB) ---");
        tarif.VyuzitaDataMB = 5120m;
        Console.WriteLine($"Po nastavení v MB: {tarif.VyuzitaDataGB} GB ({tarif.VyuzitaDataMB} MB)");

        // 1.3 Použití metody ZmenStav (přepnutí stavu Aktivní <-> Pozastavený)
        Console.WriteLine("\n--- Změna stavu tarifu (Aktivní -> Pozastavený) ---");
        tarif.ZmenStav();
        Console.WriteLine($"Nový stav tarifu: {tarif.Stav}");

        // Pokus o spotřebu dat při pozastaveném tarifu (ověření výjimky)
        Console.WriteLine("Pokus o spotřebu dat v pozastaveném stavu:");
        try
        {
            tarif.PridejSpotrebovanaData(1.0m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OŠETŘENÁ CHYBA] {ex.Message}");
        }

        // Přepnutí tarifu zpět do aktivního stavu
        Console.WriteLine("\n--- Změna stavu tarifu zpět na Aktivní ---");
        tarif.ZmenStav();
        Console.WriteLine($"Nový stav tarifu: {tarif.Stav}");

        // 1.4 Testování ochrany v setteru: Pokus o překročení měsíčního limitu
        Console.WriteLine("\n--- Test ochrany mezí: Pokus o překročení limitu 10 GB ---");
        try
        {
            // Již máme vyčerpáno 5 GB, pokus o přidání dalších 6 GB (celkem 11 GB > 10 GB)
            tarif.PridejSpotrebovanaData(6.0m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OŠETŘENÁ CHYBA] {ex.Message}");
        }

        // 1.5 Testování ochrany v setteru: Pokus o snížení limitu pod aktuální spotřebu
        Console.WriteLine("\n--- Test ochrany mezí: Pokus o snížení limitu pod aktuální spotřebu (5 GB) ---");
        try
        {
            tarif.MesicniLimitGB = 3.0m;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OŠETŘENÁ CHYBA] {ex.Message}");
        }


        // =========================================================================
        // 2. UKÁZKA TŘÍDY Tiskarna
        // =========================================================================
        Console.WriteLine("\n\n==================================================");
        Console.WriteLine("              DEMONSTRACE: Tiskarna               ");
        Console.WriteLine("==================================================");

        // Vytvoření tiskárny s kapacitou 1000 listů a počátečním obsahem 200 listů
        Tiskarna tiskarna = new Tiskarna(kapacitaZasobniku: 1000, vychoziPocetListu: 200);
        Console.WriteLine($"Vytvořena tiskárna: Kapacita = {tiskarna.KapacitaZasobniku} listů, Vloženo = {tiskarna.AktualniPocetListu} listů, Stav = {tiskarna.Stav}");
        Console.WriteLine($"Počet listů v balíčcích: {tiskarna.PocetBalicku} balíčků");

        // 2.1 Použití metody PridejListy
        Console.WriteLine("\n--- Přidání 300 listů papíru ---");
        tiskarna.PridejListy(300);
        Console.WriteLine($"V zásobníku je nyní: {tiskarna.AktualniPocetListu} listů ({tiskarna.PocetBalicku} balíčků)");

        // 2.2 Použití metody VytiskniStranky
        Console.WriteLine("\n--- Tisk 150 stránek ---");
        tiskarna.VytiskniStranky(150);
        Console.WriteLine($"Po tisku zbývá: {tiskarna.AktualniPocetListu} listů ({tiskarna.PocetBalicku} balíčků)");

        // 2.3 Použití vlastnosti PocetBalicku (čtení i zápis)
        Console.WriteLine("\n--- Nastavení počtu listů přes balíčky (1.5 balíčku = 750 listů) ---");
        tiskarna.PocetBalicku = 1.5;
        Console.WriteLine($"Po nastavení: {tiskarna.AktualniPocetListu} listů ({tiskarna.PocetBalicku} balíčků)");

        // 2.4 Použití metody ZmenStav (přepnutí stavu Funkční <-> Zaseknutá)
        Console.WriteLine("\n--- Změna stavu tiskárny (Funkční -> Zaseknutá) ---");
        tiskarna.ZmenStav();
        Console.WriteLine($"Nový stav tiskárny: {tiskarna.Stav}");

        // Pokus o tisk na zaseknuté tiskárně (ověření výjimky)
        Console.WriteLine("Pokus o tisk při zaseknuté tiskárně:");
        try
        {
            tiskarna.VytiskniStranky(10);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OŠETŘENÁ CHYBA] {ex.Message}");
        }

        // Oprava stavu (přepnutí zpět na Funkční)
        Console.WriteLine("\n--- Změna stavu tiskárny zpět na Funkční ---");
        tiskarna.ZmenStav();
        Console.WriteLine($"Nový stav tiskárny: {tiskarna.Stav}");

        // 2.5 Testování ochrany: Tisk více stránek než kolik je v zásobníku
        Console.WriteLine("\n--- Test ochrany: Pokus o tisk 900 stránek (v zásobníku je jen 750) ---");
        try
        {
            tiskarna.VytiskniStranky(900);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OŠETŘENÁ CHYBA] {ex.Message}");
        }

        // 2.6 Testování ochrany v setteru: Pokus o přidání více papíru než je kapacita
        Console.WriteLine("\n--- Test ochrany: Pokus o přidání 500 listů (750 + 500 = 1250 > 1000 kapacita) ---");
        try
        {
            tiskarna.PridejListy(500);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OŠETŘENÁ CHYBA] {ex.Message}");
        }

        // 2.7 Testování ochrany v setteru: Pokus o snížení kapacity pod aktuální stav listů
        Console.WriteLine("\n--- Test ochrany: Pokus o zmenšení kapacity na 500 listů při 750 vložených listech ---");
        try
        {
            tiskarna.KapacitaZasobniku = 500;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OŠETŘENÁ CHYBA] {ex.Message}");
        }

        Console.WriteLine("\n==================================================");
        Console.WriteLine("                 KONEC UKÁZKY                     ");
        Console.WriteLine("==================================================");
    }
}
