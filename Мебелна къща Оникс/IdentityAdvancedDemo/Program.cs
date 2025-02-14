using IdentityAdvancedDemo.Data;
using IdentityAdvancedDemo.Data.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FurnitureDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
  .AddEntityFrameworkStores<FurnitureDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options => 
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
});
 
builder.Services.AddAuthentication()
    .AddFacebook(options => 
    {
        options.AppId = "658708255927516";
        options.AppSecret = "795acf9f3041cb193e8305bc94dcd6d9";
    });

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("MyPolicy", policy => 
    {
        policy.RequireRole("Admin");
        policy.RequireClaim("EmployeeNumber");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapRazorPages();

app.Run();
