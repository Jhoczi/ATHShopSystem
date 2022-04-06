using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using asp_front.Data;
using ath_server.Db;
using ath_server.Interfaces;
using ath_server.Models;
using ath_server.Repositories;
using ath_server.Services;

var builder = WebApplication.CreateBuilder(args);

var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var postgresConnectionString = builder.Configuration.GetConnectionString("Postgres");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(defaultConnectionString));
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(postgresConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// todo Move injection of this services to API
builder.Services.AddTransient<IShopRepository, ShopRepository>();
builder.Services.AddTransient<IShopService, ShopService>();

//builder.Services.AddTransient<IRepositoryService<Shop>>();
builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

var app = builder.Build();
await CreateDbIfNotExists(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

async Task CreateDbIfNotExists(IHost host)
{
    await using var scope = host.Services.CreateAsyncScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        await Seeder.Seed(context);
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred creating the DB.");
    }
}