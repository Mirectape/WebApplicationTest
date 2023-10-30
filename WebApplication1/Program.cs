using FluentAssertions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1;
using WebApplication1.AuthPersonApp;
using WebApplication1.Data;
using WebApplication1.DataContext;
using WebApplication1.Interfaces;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IPersonData, PersonData>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddIdentity<User, IdentityRole>().
    AddEntityFrameworkStores<PersonDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    //options.Cookie.Expiration = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
});

var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=AllView}/{id?}");
});

//app.UseHttpLogging();

app.Run();

#region oldCode
//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

//var app = builder.Build();
//app.UseStaticFiles();
//app.UseMvc(
//    r =>
//    {
//        r.MapRoute(
//            name: "default",
//            template: "{controller=Person}/{action=AllView}"
//            );
//    });

////app.MapGet("/", async (context) => await context.Response.WriteAsync("Hello World!"));

//app.Run();
#endregion