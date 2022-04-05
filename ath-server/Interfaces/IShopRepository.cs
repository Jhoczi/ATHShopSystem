using ath_server.Models;

namespace ath_server.Interfaces;

public interface IShopRepository
{
    Task<IQueryable<Shop>> GetShops();
    Task<Shop> GetShop(int id);
    Task AddShop(Shop shop);
    Task UpdateShop(Shop shop);
    Task DeleteShop(int id);
}