using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vila.Web.Models;
using Vila.Web.Services.Customer;
using Vila.Web.Services.Vila;

namespace Vila.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVilaRepository _vila;
        private readonly IAuthService _auth;
        public HomeController(IVilaRepository vila, IAuthService auth)
        {
            _vila = vila;
            _auth = auth;
        }
        [Authorize]
        public async Task<IActionResult> Index(int pageId = 1 ,string filter ="",int take = 6)
        {
            //string token = HttpContext.Session.GetString("JWTSecret");
            string token = _auth.GetJwtToken();


            var model = await _vila.search(pageId, filter, take, token);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}