namespace PracA;

public class Player
{
    public Player(string name, int health = 100, int attack = 10, int defence = 5)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Defence = defence;
    }

    public string Name { get; set; }
    int Health { get; set; }
    int Attack { get; set; }
    int Defence { get; set; }

    public override string ToString()
    {
        string info = $"""
            Name: {Name} the mighty!
            Stats: HP={Health}, ATK={Attack}, DEF={Defence}
            """;
        return info;
    }
}
