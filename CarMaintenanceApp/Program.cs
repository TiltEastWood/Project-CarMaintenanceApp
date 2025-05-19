using CarMaintenanceApp.Components;
using CarMaintenanceApp.Models;
using CarMaintenanceApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CarMaintenanceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.Cookie.Name = "auth_token";
                    options.LoginPath = "/login";
                    options.Cookie.MaxAge = TimeSpan.FromDays(30);
                    options.AccessDeniedPath = "/access-denied";
                });
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<CarMaintenanceContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<ServiceRecordService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AuthServices>();
            builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            builder.Services.AddLogging();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = 10485760;
                serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2);
                serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                serverOptions.Limits.Http2.KeepAlivePingDelay = TimeSpan.FromMinutes(1);
                serverOptions.Limits.Http2.KeepAlivePingTimeout = TimeSpan.FromMinutes(1);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
