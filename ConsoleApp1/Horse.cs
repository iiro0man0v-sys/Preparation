namespace ConsoleApp1;

class Horse : IEquatable<Horse>, IComparable<Horse>
{
    public Horse(string? name, string? rider, int num)
    {
        Name = name;
        Rider_name = rider;
        Number = num;
    }

    public string? Name { get; set; }
    public string? Rider_name { get; set; }
    public int Number { get; set; }

    public int CompareTo(Horse? other)
    {
        if (other is null) return 1;
        if (ReferenceEquals(this, other)) return 0;

        int horse_n = string.Compare(Name, other.Name, StringComparison.CurrentCulture);
        if (horse_n != 0) return horse_n;

        int rider_n = string.Compare(Rider_name, other.Rider_name, StringComparison.CurrentCulture);
        if (rider_n != 0) return rider_n;

        return Number.CompareTo(other.Number);
    }

    public bool Equals(Horse? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Number == other.Number;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Horse);
    }

    public override int GetHashCode()
    {
        return Number.GetHashCode();
    }

    public override string ToString()
    {
        return $"Kůň: {Name} (Jezdec: {Rider_name}, Číslo: {Number})";
    }
}
