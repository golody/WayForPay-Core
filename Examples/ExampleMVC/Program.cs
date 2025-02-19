using ExampleMVC.Models;
using Microsoft.EntityFrameworkCore;
using WayForPay.Domain;
using WayForPay.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add your Merchant Settings
builder.Services.Configure<MerchantSettings>(builder.Configuration.GetSection("WayForPay"));
// Add WayForPay 
builder.Services.AddWayForPay();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ExampleDbContext>();
var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();