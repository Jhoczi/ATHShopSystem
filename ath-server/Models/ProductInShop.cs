namespace ath_server.Models;

public class ProductInShop
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ShopId { get; set; }
    public decimal PriceInShop { get; set; }
}