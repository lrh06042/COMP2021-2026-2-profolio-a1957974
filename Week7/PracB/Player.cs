public class Player
{
    public Player(
        string name,
        int health = 100,
        int attack = 10,
        int defence = 5)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Defence = defence;
    }

    public string Name { get; set; }
    private int Health { get; set; }
    private int Attack { get; set; }
    private int Defence { get; set; }

    public override string ToString()
    {
        return $"Name: {Name} the mighty!\n" +
               $"Stats: HP={Health}, ATK={Attack}, DEF={Defence}";
    }
}