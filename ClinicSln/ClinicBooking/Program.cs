
using ClinicBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SportsStore.Models;
using SportsStore.Models.SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ClinicBookingConnection"]);
});

builder.Services.AddScoped<IBookRepository, EFBookRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
