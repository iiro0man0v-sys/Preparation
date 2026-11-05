namespace ConsoleApp1;

class Mineral
{
    public Mineral(string? name, MineralType mineralType)
    {
        Name = name;
        Type = mineralType;
    }

    public string? Name { get; set; }
    public MineralType Type { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Type})";
    }
}
