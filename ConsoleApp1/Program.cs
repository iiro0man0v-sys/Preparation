namespace ConsoleApp1;

class Program
{
    static void Main()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("1. TEST: STUDENTI (HashSet & SortedSet)");
        Console.WriteLine("==================================================");

        // Test HashSet (unikátnost podle čísla žáka)
        var studentHashSet = new HashSet<Student>
        {
            new Student("Jan", "Novák", 1),
            new Student("Petr", "Svoboda", 2),
            new Student("Eva", "Dvořáková", 3)
        };

        bool studentDuplicateAdded = studentHashSet.Add(new Student("Karel", "Novák", 1));
        Console.WriteLine($"Pokus o přidání žáka se stejným číslem 1: {studentDuplicateAdded} (očekáváno: False)");
        Console.WriteLine($"Počet žáků v HashSetu: {studentHashSet.Count} (očekáváno: 3)");

        // Test SortedSet (řazení: Příjmení -> Jméno -> Číslo)
        var studentSortedSet = new SortedSet<Student>
        {
            new Student("Jan", "Novák", 1),
            new Student("Adam", "Novák", 4),
            new Student("Petr", "Svoboda", 2),
            new Student("Barbora", "Dvořáková", 5),
            new Student("Alena", "Dvořáková", 3)
        };

        Console.WriteLine("\nSeřazení žáci v SortedSet:");
        foreach (var s in studentSortedSet)
        {
            Console.WriteLine($"  {s}");
        }

        Console.WriteLine("\n==================================================");
        Console.WriteLine("2. TEST: KNIHOVNA (Deweyův desetinný systém)");
        Console.WriteLine("==================================================");

        var library = new Library();
        library.Add(new Book("Stručné dějiny času", "Stephen Hawking", BookType.Přírodní_vědy));
        library.Add(new Book("Původ druhů", "Charles Darwin", BookType.Přírodní_vědy));
        library.Add(new Book("Ústava", "Platón", BookType.Filozofie));
        library.Add(new Book("Kritika čistého rozumu", "Immanuel Kant", BookType.Filozofie));
        library.Add(new Book("Válka a mír", "Lev Nikolajevič Tolstoj", BookType.Literatura_a_rétorika));

        Console.WriteLine("Knihy v kategorii Přírodní vědy (500):");
        foreach (var b in library.Search(BookType.Přírodní_vědy))
        {
            Console.WriteLine($"  - {b}");
        }

        Console.WriteLine("\nKnihy v kategorii Filozofie (100) vyhledané číslem 100:");
        foreach (var b in library.Search(100))
        {
            Console.WriteLine($"  - {b}");
        }

        Console.WriteLine("\n==================================================");
        Console.WriteLine("3. TEST: SBĚRATEL MINERÁLŮ (Filtrování podle typu)");
        Console.WriteLine("==================================================");

        var collector = new MineralCollector("Karel Sběratel");
        collector.Add(new Mineral("Zlato", MineralType.Prvky));
        collector.Add(new Mineral("Pyrit", MineralType.Sulfidy));
        collector.Add(new Mineral("Křemen", MineralType.Oxidy));
        collector.Add(new Mineral("Korund", MineralType.Oxidy));
        collector.Add(new Mineral("Kalcit", MineralType.Uhličitany));

        Console.WriteLine($"Všechny minerály sběratele ({collector.Name}):");
        foreach (var m in collector.Minerals)
        {
            Console.WriteLine($"  - {m}");
        }

        Console.WriteLine("\nFiltrování minerálů typu Oxidy:");
        foreach (var m in collector.Filter_by_type(MineralType.Oxidy))
        {
            Console.WriteLine($"  - {m}");
        }
    }
}
