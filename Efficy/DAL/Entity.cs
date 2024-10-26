namespace Efficy.DAL;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}