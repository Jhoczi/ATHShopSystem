using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using asp_front.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var postgresConnectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(defaultConnectionString));
// builder.Services.AddDbContext<DataContext>(options =>
//     options.UseNpgsql(postgreConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

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
    // await using var scope = host.Services.CreateAsyncScope();
    // var services = scope.ServiceProvider;
    // try
    // {
    //     var context = services.GetRequiredService<DataContext>();
    //     await AppSeeder.Seed(context);
    // }
    // catch (Exception e)
    // {
    //     var logger = services.GetRequiredService<ILogger<Program>>();
    //     logger.LogError(e, "An error occurred creating the DB.");
    // }
}