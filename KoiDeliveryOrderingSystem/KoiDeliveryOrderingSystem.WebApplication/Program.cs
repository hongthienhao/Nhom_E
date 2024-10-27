using KoiDeliveryOrderingSystem.WebApplication.Data;
using KoiDeliveryOrderingSystem.Respositories; // Thêm namespace của repository
using KoiDeliveryOrderingSystem.Services; // Thêm namespace của service
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký DbContext cho Entity Framework Core
builder.Services.AddDbContext<HtqlkoiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Deliverykoi"));
});

// Đăng ký các lớp Repository và Service vào DI container
// Ví dụ: nếu bạn có `OrderRepository` và `OrderService`, bạn sẽ thêm như sau:
//builder.Services.AddScoped<OrderRepository>();
//builder.Services.AddScoped<OrderService>();

// Thêm Session vào DI container
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Sử dụng HSTS trong môi trường production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt Session
app.UseSession();

// Kích hoạt Authorization (nếu cần)
app.UseAuthorization();

// Cấu hình các route cho Controller và Area
app.UseEndpoints(endpoints =>
{
    // Route cho Area (nếu có)
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    // Route mặc định
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
