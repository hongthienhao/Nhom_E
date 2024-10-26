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
// builder.Services.AddScoped<OrderRepository>();
//builder.Services.AddS coped<OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
