namespace ath_server.Interfaces;

public interface IEntityWithName<T> : IEntity<T>
{
    public T Id { get; set; }
    public string Name { get; set; }
}