using IdentityLogin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityLogin.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShoppingCartContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartContext")));

builder.Services.AddDbContext<ShoppingCartsContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartsContext")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ShoppingCartContext>();
// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductCategoriesRepo,ProductCategoriesRepo>();
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

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
