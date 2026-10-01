namespace Preparation;

// Výčtový typ pro reprezentaci možných stavů tarifu.
// Použití enumu zabraňuje zadání neplatných hodnot (např. překlepů v textu).
public enum StavTarifu
{
    Aktivni,
    Pozastaveny
}

// Třída reprezentující mobilní tarif.
// V této třídě nesmí být žádný výpis do konzole (Console.WriteLine) – veškerá komunikace s uživatelem probíhá vně třídy.
public class MobilniTarif
{
    // Privátní backing field pro měsíční limit dat v GB.
    // Slouží k uchování skutečné hodnoty, ke které přistupujeme přes chráněnou property MesicniLimitGB.
    private decimal _mesicniLimitGB;

    // Privátní backing field pro aktuálně využitá data v GB.
    // Slouží k uchování skutečné hodnoty, ke které přistupujeme přes chráněnou property VyuzitaDataGB.
    private decimal _vyuzitaDataGB;

    // Vlastnost pro měsíční limit dat v GB.
    // Tuto vlastnost JE NUTNÉ CHRÁNIT pomocí kontroly v bloku set:
    // 1. Měsíční limit nesmí být záporné číslo (záporný limit nedává fyzický ani logický smysl).
    // 2. Měsíční limit nesmí být snížen pod hodnotu již aktuálně vyčerpaných dat (jinak by vznikl nekonzistentní stav, kdy je vyčerpáno více než je limit).
    public decimal MesicniLimitGB
    {
        get => _mesicniLimitGB;
        set
        {
            // Kontrola, aby limit nebyl záporný
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Měsíční limit dat nesmí být záporný.");
            }

            // Kontrola, aby nový limit nebyl menší než již vyčerpaná data
            if (value < _vyuzitaDataGB)
            {
                throw new InvalidOperationException("Měsíční limit nelze snížit pod hodnotu již vyčerpaných dat.");
            }

            _mesicniLimitGB = value;
        }
    }

    // Vlastnost pro aktuálně využitá data v GB.
    // Tuto vlastnost JE NUTNÉ CHRÁNIT pomocí kontroly v bloku set:
    // 1. Využitá data nesmí být záporná (nelze spotřebovat záporný objem dat).
    // 2. Využitá data nesmí překročit nastavený měsíční limit (požadavek zadání na nepřekročení mezí).
    public decimal VyuzitaDataGB
    {
        get => _vyuzitaDataGB;
        set
        {
            // Kontrola, aby využitá data nebyla záporná
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Využitá data nesmí být záporná.");
            }

            // Kontrola, aby využitá data nepřekročila nastavený měsíční limit
            if (value > MesicniLimitGB)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Využitá data ({value} GB) nesmí překročit měsíční limit ({MesicniLimitGB} GB).");
            }

            _vyuzitaDataGB = value;
        }
    }

    // Vlastnost pro stav tarifu (aktivní / pozastavený).
    // Zde STAČÍ BĚŽNÁ PROPERTY (auto-implemented property) bez zvláštní validační logiky v set:
    // Typ je omezen výčtem StavTarifu (Aktivni, Pozastaveny), takže do něj nelze přiřadit libovolný neplatný text.
    public StavTarifu Stav { get; set; }

    // Vypočítávaná vlastnost pro získání a nastavení využitých dat v megabajtech (MB).
    // 1 GB = 1024 MB.
    // Nemá vlastní backing field – převádí MB na GB a naopak.
    // Při nastavování (set) se hodnota převede na GB a přiřadí do VyuzitaDataGB,
    // kde automaticky proběhne veškerá validace mezí (nesmí být záporné, nesmí překročit limit).
    public decimal VyuzitaDataMB
    {
        // Přepočet z GB na MB
        get => VyuzitaDataGB * 1024m;
        // Přepočet z MB na GB a uložení s validací
        set => VyuzitaDataGB = value / 1024m;
    }

    // Konstruktor třídy pro inicializaci nového mobilního tarifu.
    // Přijímá měsíční limit dat a volitelný počáteční stav (výchozí je Aktivní).
    // Počáteční využitá data jsou nastavena na 0.
    public MobilniTarif(decimal mesicniLimitGB, StavTarifu vychoziStav = StavTarifu.Aktivni)
    {
        // Nastavením přes property MesicniLimitGB se rovnou provede validace hodnoty
        MesicniLimitGB = mesicniLimitGB;
        // Výchozí využitá data jsou 0 GB
        VyuzitaDataGB = 0;
        // Nastavení výchozího stavu
        Stav = vychoziStav;
    }

    // Metoda pro přidání určitého množství spotřebovaných dat v GB.
    // Provádí kontroly:
    // 1. Zda přidávaný objem není záporný.
    // 2. Zda tarif není pozastavený (při pozastaveném tarifu nelze data čerpat).
    // Následné přičtení spustí validaci v set vlastnosti VyuzitaDataGB (nepřekročení limitu).
    public void PridejSpotrebovanaData(decimal objemDatGB)
    {
        // Kontrola nezápornosti vstupního parametru
        if (objemDatGB < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(objemDatGB), "Přidávaný objem dat nesmí být záporný.");
        }

        // Pokud je tarif pozastaven, nelze data spotřebovávat
        if (Stav == StavTarifu.Pozastaveny)
        {
            throw new InvalidOperationException("Nelze spotřebovávat data, tarif je pozastavený.");
        }

        // Přičtení dat (validace nepřekročení limitu proběhne v setteru VyuzitaDataGB)
        VyuzitaDataGB += objemDatGB;
    }

    // Metoda pro změnu stavu tarifu (aktivní <-> pozastavený).
    // Pokud je tarif aktivní, přepne jej na pozastavený a naopak.
    public void ZmenStav()
    {
        Stav = (Stav == StavTarifu.Aktivni) ? StavTarifu.Pozastaveny : StavTarifu.Aktivni;
    }
}