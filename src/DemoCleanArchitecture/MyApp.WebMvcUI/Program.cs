using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MyApp.Infrastructure;
using MyApp.Application;
using MyApp.Application.Common.Interfaces;
using MyApp.WebMvcUI.Services.CurrentUser;
using MyApp.WebMvcUI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ---  Add services to the container ( same as Startup class ConfigureServices() ).


//MyApp.Infrastructure.ConfigureServices.AddInfrastructureServices(
//    builder.Services, builder.Environment, builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Environment, builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


builder.Services.AddControllersWithViews();


var app = builder.Build();


// --- Configure the HTTP request pipeline.  (same as Startup class Configure() )

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Register the Custom Middleware handling Application Exceptions
app.UseMiddleware<AppExceptionMiddleware>();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
