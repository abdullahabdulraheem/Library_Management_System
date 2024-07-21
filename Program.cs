using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Library_Management_System.Data.Context;
using Library_Management_System.Service.Interface;
using Library_Management_System.Service;
using Library_Management_System.Data.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<LibraryDbContext>(
//    OptionsBuilder => OptionsBuilder.UseMySql(builder.Configuration.GetConnectionString("LMSConnectionString"), new MySqlServerVersion("8.0"))
//);

var connectionString = builder.Configuration.GetConnectionString("LMSConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(opt => 
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<LibraryDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 7;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});


builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IUserService,UserService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
