using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.Repositories;
using PRN222.Kahoot.Repository.UnitOfWork;
using PRN222.Kahoot.Service.Mappers;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to this path if unauthenticated
        options.LogoutPath = "/Account/Logout"; // Redirect to this path if logged out
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax; // Adjust based on your needs
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use Always if HTTPS, None if HTTP on localhost
        options.Cookie.MaxAge = TimeSpan.FromMinutes(10); // Adjust based on your needs
    });

builder.Services.AddDbContext<KahootContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KahootContext")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuizSessionService, QuizSessionService>();
builder.Services.AddScoped<IQuestionSessionService, QuestionSessionService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IResponseService, ResponseService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
