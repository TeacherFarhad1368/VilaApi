using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vila.Web.Models.Customer;
using Vila.Web.Services.Customer;

namespace Vila.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ICustomerRepository _customer;
        private readonly IAuthService _auth;
        public AuthController(ICustomerRepository customer, IAuthService auth)
        {
            _customer = customer;
            _auth = auth;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var res =await _customer.Register(model);
            if (res.Result)
            {
                TempData["success"] = true;
                return View();
            }
            else
            {
                ModelState.AddModelError("", res.Message);
                return View(model);
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var res = await _customer.Login(model);

            if (!res.Result.Result)
            {
                ModelState.AddModelError("", res.Result.Message);
                return View(model);
            }
            var customer = res.Customer;

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, customer.Mobile));
            identity.AddClaim(new Claim(ClaimTypes.Role, customer.Role));
            identity.AddClaim(new Claim("JWTSecret", customer.JwtSecret));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect("/");
        }

        public IActionResult Logout()
        {
            _auth.SignOut();
            return RedirectToAction("Login");
        }

        public IActionResult NotAccess()
        {
            return View();
        }
    }
}
