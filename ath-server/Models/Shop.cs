using ath_server.Interfaces;

namespace ath_server.Models;
public class Shop : IEntityWithName<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    
}