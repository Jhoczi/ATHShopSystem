using ath_server.Interfaces;
using ath_server.Models;

public class Product : IEntityWithName<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}