using ath_server.Db;
using ath_server.Interfaces;
using ath_server.Models;
using Microsoft.EntityFrameworkCore;

namespace ath_server.Repositories;

public class ShopRepository : IShopRepository,IDisposable
{
    private DataContext _dbContext;

    public ShopRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task<IQueryable<Shop>> GetShops()
    {
        return _dbContext.Shops;
    }

    public async Task<Shop> GetShop(int id)
    {
        // return await _dbContext.Shops.SingleAsync(x=>x.Id == id);
        return null;
    }

    public async Task AddShop(Shop shop)
    {
        await _dbContext.Shops.AddAsync(shop);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateShop(Shop shop)
    {
        _dbContext.Shops.Update(shop);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteShop(int id)
    {
        // var shop = await _dbContext.Shops.SingleAsync(x => x.Id == id);
        // _dbContext.Shops.Remove(shop);
        // await _dbContext.SaveChangesAsync();
        await Task.CompletedTask;
    }

    // public static ShopRepository CreateRepository()
    // {
    //     return new ShopRepository();
    // }
}