using Microsoft.EntityFrameworkCore;
using MultimodalShippingSystem.Data;
using MultimodalShippingSystem.Services;
using MultimodalShippingSystem.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShippingDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("ShippingDb");
        return;
    }

    options.UseSqlServer(builder.Configuration.GetConnectionString("ShippingDatabase"));
});

builder.Services.AddScoped<RoadShippingStrategy>();
builder.Services.AddScoped<AirShippingStrategy>();
builder.Services.AddScoped<IShippingStrategy, RoadShippingStrategy>();
builder.Services.AddScoped<IShippingStrategy, AirShippingStrategy>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<IShippingService, ShippingService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShippingDbContext>();
    dbContext.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shipments}/{action=Index}/{id?}");

app.Run();
