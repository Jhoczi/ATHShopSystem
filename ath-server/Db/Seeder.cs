using ath_server.Models;

namespace ath_server.Db;

public class Seeder
{
    public static async Task Seed(DataContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        if (context.Shops.Any())
        {
            return;
        }

        IEnumerable<Shop> shops = new List<Shop>() 
        {
            new ()
            {
                Name = "HoczBud",
                Description = "Some description 1",
                Products = new List<Product>()
                {
                    new ()
                    {
                        Name = "Coca Cola",
                        Description = "Very sweet drink",
                        Price = 3.30m,
                    },
                    new ()
                    {
                        Name = "Coca Cola Light",
                        Description = "Very sweet drink with zero calories",
                        Price = 3.50m,
                    }
                }
            },
            new ()
            {
                Name = "SuszarX",
                Description = "Some description 2",
                Products = new List<Product>()
                {
                    new ()
                    {
                        Name = "Suszar X1.0",
                        Description = "Super Suszar 1.0",
                        Price = 120.00m
                    },
                    new ()
                    {
                        Name = "Suszar X2.0",
                        Description = "Super Suszar 2.0",
                        Price = 260.00m
                    },
                    new ()
                    {
                        Name = "Suszar X2.5",
                        Description = "Super Suszar 2.5",
                        Price = 275.00m
                    },
                }
            },
            new ()
            {
                Name = "Soplicopol",
                Description = "Some description 3",
                Products = new List<Product>
                {
                    new ()
                    {
                        Name = "Soplica Biała 0.5",
                       Description = "Polish vodka",
                       Price = 26.00m,
                    },
                    new ()
                    {
                        Name = "Soplica Biała 0.7",
                        Description = "Polish vodka",
                        Price = 32.00m,
                    },
                    new ()
                    {
                        Name = "Żubrowka Biała",
                        Description = "Polish vodka 0.5",
                        Price = 21.00m,
                    }
                }
            }
        };

        await context.AddRangeAsync(shops);
        await context.SaveChangesAsync();
    }
}