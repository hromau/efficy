namespace Efficy.DAL;

public class Counter : Entity
{
    public int Steps { get; set; }
    public string? Description { get; set; }
    public virtual Team? Team { get; set; }
}