using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentWorkshopPortal.Data;
using StudentWorkshopPortal.Repositories;
using StudentWorkshopPortal.Repositories.Interfaces;
using StudentWorshopPortal.Data.Import;
using StudentWorshopPortal.Middleware;
using StudentWorshopPortal.Models;
using StudentWorshopPortal.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<WorkshopService>();
builder.Services.AddScoped<RegistrationService>();

builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<JsonDataSeeder>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("StudentWorkshopDb"),
        sql =>
        {
            sql.MigrationsAssembly(
                typeof(ApplicationDbContext).Assembly.FullName
            );

            sql.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            );
        }
    );
});

/*
 * Configure ASP.NET Core Identity.
 *
 * ApplicationUser is the custom student account.
 * IdentityRole enables Identity's role tables.
 * ApplicationDbContext stores Identity and application data
 * in the same SQL Server database.
 */
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

/*
 * Configure the Identity authentication cookie.
 */
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/access-denied";

    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await context.Database.MigrateAsync();

    var seeder = scope.ServiceProvider
        .GetRequiredService<JsonDataSeeder>();

    await seeder.SeedAsync();
}

app.UseExceptionHandler("/Home/Error");

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<MaintenanceModeMiddleware>();

app.UseRouting();

/*
 * Authentication must come before authorization.
 */
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();