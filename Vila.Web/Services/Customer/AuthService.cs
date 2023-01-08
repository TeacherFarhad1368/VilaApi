using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Vila.Web.Services.Customer
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _context;
        public AuthService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetJwtToken()
        {
            var claims = _context.HttpContext.User.Claims.ToList();
            if (claims.Count() < 1) return "";
            return _context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "JWTSecret").Value;
        }

        public void SignOut()
        {
            _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
