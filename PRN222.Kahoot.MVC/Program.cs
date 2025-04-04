using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.Repositories;
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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Chỉnh lại thành đúng đường dẫn trang login
        options.LogoutPath = "/Login/Logout"; // Nếu có trang logout thì giữ nguyên
        options.AccessDeniedPath = "/Login"; // Nếu quyền truy cập bị từ chối, cũng đưa về Login
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.MaxAge = TimeSpan.FromMinutes(10);
    });


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
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Login");
        return;
    }
    await next();
});

app.Run();