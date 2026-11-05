namespace ConsoleApp1;

class MineralCollector
{
    public MineralCollector(string? name)
    {
        Name = name;
        Minerals = new List<Mineral>();
    }

    public string? Name { get; set; }
    private List<Mineral> Minerals;

    public List<Mineral> Add(Mineral m)
    {
        Minerals.Add(m);
        return Minerals;
    }

    public List<Mineral> Filter_by_type(MineralType type)
    {
        List<Mineral> fitered_minerals = new List<Mineral>();
        foreach (var m in Minerals)
        {
            if (m.Type == type)
            {
                fitered_minerals.Add(m);
            }
        }
        return fitered_minerals;
    }

    public override string ToString()
    {
        return $"{Name} (celkem minerálů: {Minerals.Count})";
    }
}
