using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.UnitOfWork;
using PRN222.Kahoot.Service.Interfaces;
using PRN222.Kahoot.Service.Mappers;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext for Entity Framework Core
builder.Services.AddDbContext<KahootContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KahootContext")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

// Add UnitOfWork and Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ISessionService, SessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();