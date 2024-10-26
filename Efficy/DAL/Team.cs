namespace Efficy.DAL;

public class Team : Entity
{
    public string? Name { get; set; }
    public virtual List<Counter> Counters { get; set; } = new List<Counter>();
}