namespace ath_server.Interfaces;

public interface IEntity<T>
{
    public T Id { get; set; }
}