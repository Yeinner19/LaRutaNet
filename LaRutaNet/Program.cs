using Microsoft.EntityFrameworkCore;
using LaRutaNet.Models;

using Microsoft.AspNetCore.Identity;
using LaRutaNet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var conString = builder.Configuration.GetConnectionString("conexion") ??
     throw new InvalidOperationException("Connection string 'conexion'" +
    " not found.");
builder.Services.AddDbContext<LarutaContext>(options =>
      options.UseMySql(conString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.44-mysql")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(conString, ServerVersion.AutoDetect(conString)));

// Configuraci�n de Identity
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        // Reglas de contrase�a relajadas para desarrollo
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
