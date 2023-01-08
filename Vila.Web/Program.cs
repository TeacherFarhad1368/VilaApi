using Microsoft.AspNetCore.Authentication.Cookies;
using Vila.Web.Services.Customer;
using Vila.Web.Services.Detail;
using Vila.Web.Services.Vila;
using Vila.Web.Utility;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddCors();
services.AddControllersWithViews();
services.AddHttpClient();
#region Auth
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.Cookie.HttpOnly = true;
        x.ExpireTimeSpan = TimeSpan.FromDays(7);
        x.LoginPath = "/Auth/Login";
        x.LogoutPath = "/Auth/Logout";
        x.AccessDeniedPath = "/Auth/NotAccess";
    });
services.AddHttpContextAccessor();
#endregion
#region Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion
#region ApiUrls
var ApiUrlsSection = builder.Configuration.GetSection("ApiUrls");
services.Configure<ApiUrls>(ApiUrlsSection);
#endregion
#region Dependency
services.AddTransient<ICustomerRepository, CustomerRepository>();
services.AddTransient<IVilaRepository, VilaRepository>();
services.AddTransient<IDetailRepository, DetailRepository>();
services.AddTransient<IAuthService, AuthService>();
#endregion
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
