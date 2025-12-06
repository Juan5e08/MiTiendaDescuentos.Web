using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiTiendaDescuentos.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// -----------------
// 1. Base de datos
// -----------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// -----------------
// 2. Identity + cookies
// -----------------
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // No exigir confirmación de correo para poder entrar
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Configuración de la cookie de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

    // La sesión dura 10 minutos
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.SlidingExpiration = false;   // No se renueva con cada petición
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// -----------------
// 3. MVC + política global de autorización
// -----------------
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()   // Todo requiere estar logueado
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

// -----------------
// 4. Razor Pages (área Identity)
//    Permitimos anónimo solo en Login y Register
// -----------------
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Login");
    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Register");
    // Logout NO se deja anónimo: debe estar autenticado para cerrar sesión
});

var app = builder.Build();

// -----------------
// 5. Pipeline HTTP
// -----------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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