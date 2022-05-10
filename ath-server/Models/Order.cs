namespace ath_server.Models;
public class Order
{
    public int Id { get; set; }
    public Shop shop { get; set; }
    public DateTime CreationDate { get; set; }
    public OrderStatus Status { get; set; }
    public virtual ICollection<OrderDescription> OrderDescriptions { get; set; }
}

public enum OrderStatus
{
    Started,
    InRealization,
    InProgress,
    Finished
}