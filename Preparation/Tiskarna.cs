namespace Preparation;

// Výčtový typ pro reprezentaci možných stavů tiskárny.
// Použitím enumu zajistíme, že tiskárna může nabývat pouze platných stavů (Funkcni nebo Zaseknuta).
public enum StavTiskarny
{
    Funkcni,
    Zaseknuta
}

// Třída reprezentující stolní tiskárnu.
// V samotné třídě se nevyskytuje žádný výpis do konzole (Console.WriteLine) – veškeré výstupy řeší volající kód (Main).
public class Tiskarna
{
    // Privátní backing field pro celkovou kapacitu zásobníku papíru (počet listů).
    // K hodnotě se přistupuje výhradně přes property KapacitaZasobniku s příslušnou kontrolou mezí.
    private int _kapacitaZasobniku;

    // Privátní backing field pro aktuální počet listů papíru v zásobníku.
    // K hodnotě se přistupuje přes property AktualniPocetListu s kontrolou mezí (nesmí být záporná ani vyšší než kapacita).
    private int _aktualniPocetListu;

    // Vlastnost pro kapacitu zásobníku papíru (maximální počet listů).
    // Tuto vlastnost JE NUTNÉ CHRÁNIT pomocí kontroly v bloku set:
    // 1. Kapacita nesmí být záporné číslo (zásobník nemůže pojmout záporný počet listů).
    // 2. Kapacitu nelze zmenšit pod aktuální počet listů v zásobníku (jinak by došlo k přetečení zásobníku a nekonzistenci).
    public int KapacitaZasobniku
    {
        get => _kapacitaZasobniku;
        set
        {
            // Kontrola, aby kapacita nebyla záporná
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Kapacita zásobníku nesmí být záporná.");
            }

            // Kontrola, zda nová kapacita není menší než množství papíru, které už v tiskárně leží
            if (value < _aktualniPocetListu)
            {
                throw new InvalidOperationException("Kapacitu zásobníku nelze nastavit na méně, než kolik listů je aktuálně vloženo.");
            }

            _kapacitaZasobniku = value;
        }
    }

    // Vlastnost pro aktuální počet listů v zásobníku.
    // Tuto vlastnost JE NUTNÉ CHRÁNIT pomocí kontroly v bloku set:
    // 1. Počet listů nesmí být záporné číslo (v zásobníku nemůže být méně než 0 listů).
    // 2. Počet listů nesmí překročit maximální kapacitu zásobníku (fyzické omezení zásobníku).
    public int AktualniPocetListu
    {
        get => _aktualniPocetListu;
        set
        {
            // Kontrola, aby počet listů nebyl záporný
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Počet listů nesmí být záporný.");
            }

            // Kontrola, aby počet listů nepřekročil maximální kapacitu zásobníku
            if (value > KapacitaZasobniku)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Počet listů ({value}) nesmí překročit kapacitu zásobníku ({KapacitaZasobniku}).");
            }

            _aktualniPocetListu = value;
        }
    }

    // Vlastnost pro stav tiskárny (funkční / zaseknutá).
    // Zde STAČÍ BĚŽNÁ PROPERTY (auto-implemented property) bez zvláštní validační logiky v set:
    // Typ je omezen výčtovým typem StavTiskarny (Funkcni, Zaseknuta), což samo o sobě garantuje platné hodnoty.
    public StavTiskarny Stav { get; set; }

    // Vypočítávaná vlastnost pro získání a nastavení počtu listů v celých či necelých balíčcích (1 balíček = 500 listů).
    // Typ double volíme proto, aby bylo možné pracovat i se zlomky balíčků (např. 250 listů = 0.5 balíčku).
    // Při nastavování (set) se balíčky přepočtou na listy (zaokrouhleno na celé listy) a uloží do AktualniPocetListu,
    // kde proběhne standardní kontrola mezí (nezápornost a nepřekročení kapacity).
    public double PocetBalicku
    {
        // Přepočet z počtu listů na balíčky
        get => (double)AktualniPocetListu / 500.0;
        // Přepočet z balíčků na listy s zaokrouhlením a uložením s validací
        set => AktualniPocetListu = (int)Math.Round(value * 500.0);
    }

    // Konstruktor třídy pro vytvoření a inicializaci tiskárny.
    // Přijímá kapacitu zásobníku, volitelný počáteční počet listů (výchozí je 0) a stav (výchozí je Funkcni).
    public Tiskarna(int kapacitaZasobniku, int vychoziPocetListu = 0, StavTiskarny vychoziStav = StavTiskarny.Funkcni)
    {
        // Zkontrolujeme a nastavíme nejprve kapacitu zásobníku
        if (kapacitaZasobniku < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(kapacitaZasobniku), "Kapacita zásobníku nesmí být záporná.");
        }

        KapacitaZasobniku = kapacitaZasobniku;
        // Nastavení výchozího počtu listů (proběhne kontrola vůči právě nastavené kapacitě)
        AktualniPocetListu = vychoziPocetListu;
        // Nastavení výchozího stavu
        Stav = vychoziStav;
    }

    // Metoda pro přidání určitého množství listů do zásobníku.
    // Provádí kontrolu:
    // 1. Zda přidávaný počet není záporný.
    // Následné přičtení probíhá přes property AktualniPocetListu, která zajistí, že nepřekročíme kapacitu.
    public void PridejListy(int pocetListu)
    {
        // Kontrola nezápornosti vstupního parametru
        if (pocetListu < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pocetListu), "Přidávaný počet listů nesmí být záporný.");
        }

        // Přičtení listů do zásobníku (pokud by součet překročil kapacitu, setter vyvolá výjimku)
        AktualniPocetListu += pocetListu;
    }

    // Metoda pro tisk určitého počtu stránek, která ubírá papír ze zásobníku.
    // Provádí kontroly:
    // 1. Zda požadovaný počet stránek není záporný.
    // 2. Zda tiskárna není zaseknutá (při zaseknutí nelze tisknout).
    // 3. Zda je v zásobníku dostatek papíru pro požadovaný tisk.
    public void VytiskniStranky(int pocetStran)
    {
        // Kontrola nezápornosti vstupního parametru
        if (pocetStran < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pocetStran), "Počet stránek k vytištění nesmí být záporný.");
        }

        // Pokud je tiskárna zaseknutá, tisk není možný
        if (Stav == StavTiskarny.Zaseknuta)
        {
            throw new InvalidOperationException("Nelze tisknout, tiskárna je zaseknutá.");
        }

        // Pokud není v zásobníku dostatek listů, tisk nelze dokončit
        if (pocetStran > AktualniPocetListu)
        {
            throw new InvalidOperationException($"Nedostatek papíru v zásobníku. Požadováno: {pocetStran}, k dispozici: {AktualniPocetListu}.");
        }

        // Odečtení vytištěných listů ze zásobníku
        AktualniPocetListu -= pocetStran;
    }

    // Metoda pro změnu stavu tiskárny (funkční <-> zaseknutá).
    // Pokud je tiskárna funkční, přepne se na zaseknutou a naopak (např. při uvolnění zaseknutého papíru).
    public void ZmenStav()
    {
        Stav = (Stav == StavTiskarny.Funkcni) ? StavTiskarny.Zaseknuta : StavTiskarny.Funkcni;
    }
}
