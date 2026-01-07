using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Service;

var builder = WebApplication.CreateBuilder(args);

//  Project Controller
builder.Services.AddControllersWithViews();

//  Project Services
builder.Services.AddScoped<IAuthUser, AuthService>();
builder.Services.AddScoped<IGenerateCustomId, AuthService>();

//  Project Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=Lagoon.db");
});

//  Project Cookies Config
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //  Default endpoint when [AUTHORIZE] endpoints are accessed incorrectly
        options.LoginPath = "/Account/Login";
        //options.AccessDeniedPath = "/Account/AccessDenied";

        //  Cookie Duration
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

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

app.UseAuthentication();    //  Checks Cookie
app.UseAuthorization();     //  Checks for [AUTHORIZE] annotations

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LandingPage}/{id?}");

app.Run();
