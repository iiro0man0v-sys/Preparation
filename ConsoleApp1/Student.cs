namespace ConsoleApp1;

public class Student : IComparable<Student>, IEquatable<Student>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Number { get; set; }

    public Student(string? name, string? surname, int number)
    {
        Name = name;
        Surname = surname;
        Number = number;
    }

    /// <summary>
    /// Řazení pro SortedSet:
    /// 1. Podle příjmení
    /// 2. Při shodě příjmení podle křestního jména
    /// 3. Při shodě celého jména podle čísla žáka (zajišťuje konzistenci se SortedSet a zabraňuje ztrátě různých žáků se stejným jménem)
    /// </summary>
    public int CompareTo(Student? other)
    {
        if (other is null) return 1;
        if (ReferenceEquals(this, other)) return 0;

        int lastNameComparison = string.Compare(Surname, other.Surname, StringComparison.CurrentCulture);
        if (lastNameComparison != 0)
        {
            return lastNameComparison;
        }

        int firstNameComparison = string.Compare(Name, other.Name, StringComparison.CurrentCulture);
        if (firstNameComparison != 0)
        {
            return firstNameComparison;
        }

        return Number.CompareTo(other.Number);
    }

    /// <summary>
    /// Rovnost pro HashSet:
    /// Každý žák má unikátní číslo ve třídním výkazu.
    /// </summary>
    public bool Equals(Student? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Number == other.Number;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Student);
    }

    public override int GetHashCode()
    {
        return Number.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Number}: {Surname} {Name}";
    }
}
