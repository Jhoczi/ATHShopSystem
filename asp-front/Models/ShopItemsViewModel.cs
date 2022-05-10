using ath_server.Models;
namespace asp_front.Models;
public class ShopItemsViewModel
{
    public List<ShopViewModel> Shops;
    public List<global::Product> Products { get; set; }
}