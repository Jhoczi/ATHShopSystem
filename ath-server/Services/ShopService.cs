using ath_server.Interfaces;
using ath_server.Models;

namespace ath_server.Services;

public class ShopService : IShopService
{
    IShopRepository _shopRepository;
    public ShopService(IShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }
    
    public async Task<List<Shop>> GetAll()
    {
        return (await _shopRepository.GetShops()).ToList();
    }

    public async Task<Shop> GetById(int id)
    {
        return (await _shopRepository.GetShop(id));
    }
}