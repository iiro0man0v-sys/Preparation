namespace ConsoleApp1;

class Program
{
    static void Main()
    {
        var library = new Library();

        // Přidání knih různých kategorií
        library.Add(new Book("Stručné dějiny času", "Stephen Hawking", BookType.Přírodní_vědy));
        library.Add(new Book("Původ druhů", "Charles Darwin", BookType.Přírodní_vědy));
        library.Add(new Book("Ústava", "Platón", BookType.Filozofie));
        library.Add(new Book("Kritika čistého rozumu", "Immanuel Kant", BookType.Filozofie));
        library.Add(new Book("Válka a mír", "Lev Nikolajevič Tolstoj", BookType.Literatura_a_rétorika));
        library.Add(new Book("Struktury a algoritmy v C#", "John Doe", BookType.Všeobecné));

        // Test vyhledávání podle třídy
        Console.WriteLine("=== Vyhledání knih pro: Přírodní vědy (500) ===");
        var naturalSciences = library.Search(BookType.Přírodní_vědy);
        foreach (var book in naturalSciences)
        {
            Console.WriteLine($"  - {book}");
        }

        Console.WriteLine("\n=== Vyhledání knih pro: Filozofie (100) přes číselný kód ===");
        var philosophy = library.Search(100);
        foreach (var book in philosophy)
        {
            Console.WriteLine($"  - {book}");
        }

        Console.WriteLine("\n=== Vyhledání prázdné kategorie: Umění (700) ===");
        var arts = library.Search(BookType.Umění);
        Console.WriteLine($"Počet nalezených knih: {arts.Count}");
    }
}
