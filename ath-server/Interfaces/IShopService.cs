using ath_server.Models;

namespace ath_server.Interfaces;

public interface IShopService
{
    Task<List<Shop>> GetAll();
    Task<Shop> GetById(int id);
}