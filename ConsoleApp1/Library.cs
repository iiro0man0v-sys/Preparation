namespace ConsoleApp1;

class Library
{
    // Vhodná kolekce pro rychlé vyhledávání podle Deweyova systému O(1):
    // Klíč = třída Deweyova systému (BookType)
    // Hodnota = seznam knih zařazených do této třídy
    private Dictionary<BookType, List<Book>> _booksByClass;

    public Library()
    {
        _booksByClass = new Dictionary<BookType, List<Book>>();
    }

    /// <summary>
    /// Metoda pro uložení nové knihy do knihovny.
    /// </summary>
    public void Add(Book? book)
    {
        if (book is null) return;

        // Pokud pro danou třídu ještě neexistuje seznam, vytvoříme ho
        if (!_booksByClass.ContainsKey(book.BookType))
        {
            _booksByClass[book.BookType] = new List<Book>();
        }

        _booksByClass[book.BookType].Add(book);
    }

    /// <summary>
    /// Metoda, která vrátí seznam všech knih zadané třídy.
    /// </summary>
    public List<Book> Search(BookType bookType)
    {
        // Okamžité vyhledání v O(1) bez nutnosti procházet všechny knihy
        if (_booksByClass.TryGetValue(bookType, out var books))
        {
            return new List<Book>(books);
        }

        // Pokud žádné knihy v této třídě nejsou, vrátíme prázdný seznam
        return new List<Book>();
    }

    /// <summary>
    /// Přetížení metody Search pro vyhledávání přímo podle číselného kódu (např. 100, 500).
    /// </summary>
    public List<Book> Search(int deweyCode)
    {
        return Search((BookType)deweyCode);
    }
}
