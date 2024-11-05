
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Implementations;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services;
using KoiDeliveryOrderingSystem.Services.Implementations;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext cho Entity Framework Core với SQL Server
builder.Services.AddDbContext<HTQLKoiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Deliverykoi"));
});

// Thêm dịch vụ cho các Repository và Service vào DI container (Dependency Injection)
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


// Thêm dịch vụ cho Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Thêm các dịch vụ cho MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Kiểm tra môi trường để cấu hình pipeline xử lý lỗi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Sử dụng HSTS trong môi trường production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt Session trong pipeline
app.UseSession();

// Kích hoạt Authorization (nếu cần)
app.UseAuthorization();

// Cấu hình các route cho ứng dụng
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
