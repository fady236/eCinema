using eCinema.Data;
using eCinema.Data.Cart;
using eCinema.Data.Services;
using eCinema.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ قراءة ConnectionString من appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // ✅ استخدام المتغير مباشرة

// ✅ تسجيل الخدمات في DI Container
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//Authentication and Authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
// ✅ تفعيل المصادقة باستخدام Cookies
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ تفعيل الميدل وير (Middleware)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ✅ تفعيل الجلسات قبل المصادقة
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// ✅ تهيئة قاعدة البيانات بالبيانات الأولية
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRoleAsync(app).Wait();

// ✅ إعداد Route الافتراضي ليكون صفحة Movies بدلاً من Home
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.Run();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await AppDbInitializer.SeedUsersAndRoleAsync(app);
}

