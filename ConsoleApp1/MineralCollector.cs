namespace ConsoleApp1;

class MineralCollector
{
    public string? Name { get; set; }
    public List<Mineral> Minerals { get; set; }

    public MineralCollector(string? name)
    {
        Name = name;
        Minerals = new List<Mineral>();
    }

    /// <summary>
    /// Přidá nový minerál do sbírky a vrátí aktuální seznam minerálů.
    /// </summary>
    public List<Mineral> Add(Mineral m)
    {
        Minerals.Add(m);
        return Minerals;
    }

    /// <summary>
    /// Vyfiltruje a vrátí všechny minerály zadaného typu.
    /// </summary>
    public List<Mineral> Filter_by_type(MineralType type)
    {
        List<Mineral> filteredMinerals = new List<Mineral>();
        foreach (var m in Minerals)
        {
            if (m.Type == type)
            {
                filteredMinerals.Add(m);
            }
        }
        return filteredMinerals;
    }

    public override string ToString()
    {
        return $"{Name} (celkem minerálů: {Minerals.Count})";
    }
}
