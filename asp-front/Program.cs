using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ath_server.Db;
using ath_server.Interfaces;
using ath_server.Repositories;
using ath_server.Services;

var builder = WebApplication.CreateBuilder(args);

//var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(defaultConnectionString));
// Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlite(defaultConnectionString));
var postgresConnectionString = builder.Configuration.GetConnectionString("Postgres");
// Localization Settings:
var supportedCultures = new[] {"en-US", "pl"};
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
// Builder Services:
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(postgresConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(0);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// todo Move injection of this services to API
builder.Services.AddTransient<IShopRepository, ShopRepository>();
builder.Services.AddTransient<IShopService, ShopService>();

// todo zajecia z doktorem:
builder.Services.AddScoped<IAuthorizationInitializer, AuthorizationInitializer>();
//builder.Services.AddScoped<IAuthorizationInitializer, AuthorizationInitializer>();

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
app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name : "areas",
        pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapAreaControllerRoute(
    name: "MyAreManageAdmin",
    areaName: "MainAdmin",
    pattern: "MainAdmin/{controller=Home}/{action=Index}/{id?}");

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
        var authorizationInitializer = services.GetRequiredService<IAuthorizationInitializer>();
        await Seeder.Seed(context);
        await authorizationInitializer.GenerateAdminAndRoles();
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred creating the DB.");
    }
}