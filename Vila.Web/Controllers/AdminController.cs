using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vila.Web.Services.Customer;
using Vila.Web.Services.Vila;
using Vila.Web.Models.Vila;
using Vila.Web.Utility;
using Vila.Web.Services.Detail;
using Vila.Web.Models.Detail;

namespace Vila.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IVilaRepository _vila;
        private readonly IDetailRepository _detail;
        private readonly IAuthService _auth;
        private readonly ApiUrls _apiUrl;
        public AdminController(IVilaRepository vila, IDetailRepository detail , IOptions<ApiUrls> apiUrl, IAuthService auth)
        {
            _vila = vila;
            _apiUrl = apiUrl.Value;
            _auth = auth;
            _detail = detail;
        }
        #region Vila
        public async Task<IActionResult> AllVilas()
        {
            var secret = _auth.GetJwtToken();
            var url = _apiUrl.BaseAddress + _apiUrl.VilaV1Address;
            var model = await _vila.GetAll(url, secret);
            return View(model);
        }

        public IActionResult AddVila()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVila(VilaModel vila, IFormFile picture)
        {
            if (!ModelState.IsValid) return View(vila);

            try
            {
                DateTime date = vila.BuildDate.ToEnglishDateTime();
            }
            catch
            {
                ModelState.AddModelError("BuildDate", "فرمت ناریخ باید 1365/04/01 باشد.");
                return View(vila);
            }

            if (picture == null || !picture.IsImage())
            {
                ModelState.AddModelError("Image", "لطفا عکس با فرمت jpg واردکنید .");
                return View(vila);
            }

            //convert picture to byte[]

            using (var open = picture.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                open.CopyTo(ms);
                vila.Image = ms.ToArray();
            }


            var secret = _auth.GetJwtToken();
            var url = _apiUrl.BaseAddress + _apiUrl.VilaV1Address;

            bool create = await _vila.Create(url, secret, vila);

            if (create)
                TempData["success"] = true;

            return RedirectToAction("AllVilas");
        }

        public async Task<IActionResult> EditVila(int id)
        {
            var secret = _auth.GetJwtToken();
            var url = $"{_apiUrl.BaseAddress}{_apiUrl.VilaV1Address}/GetDetails/{id}";
            var vila = await _vila.GetById(url, secret);
            return View(vila);
        }

        [HttpPost]
        public async Task<IActionResult> EditVila(int id, VilaModel vila, IFormFile? picture)
        {
            if (!ModelState.IsValid) return View(vila);
            if (picture != null)
            {
                if (!picture.IsImage())
                {
                    ModelState.AddModelError("Image", "لطفا عکس با فرمت jpg واردکنید .");
                    return View(vila);
                }
                using (var open = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    open.CopyTo(ms);
                    vila.Image = ms.ToArray();
                }
            }
            var secret = _auth.GetJwtToken();
            var url = $"{_apiUrl.BaseAddress}{_apiUrl.VilaV1Address}/{id}";
            bool update = await _vila.Update(url, secret, vila);

            if (update)
                TempData["success"] = true;

            return RedirectToAction("AllVilas");
        }

        public async Task<IActionResult> DeleteVila(int id)
        {
            var secret = _auth.GetJwtToken();
            var url = $"{_apiUrl.BaseAddress}{_apiUrl.VilaV1Address}/{id}";

            bool delete = await _vila.Delete(url, secret);

            if (delete)
                TempData["success"] = true;

            return RedirectToAction("AllVilas");
        }
        #endregion

        #region Detail 

        public async Task<IActionResult> GetVilaDetails(int id)
        {
            var secret = _auth.GetJwtToken();

            var urlVila = $"{_apiUrl.BaseAddress}{_apiUrl.VilaV1Address}/GetDetails/{id}";
            var vila = await _vila.GetById(urlVila, secret);
            if (vila == null) return NotFound();


            var url = $"{_apiUrl.BaseAddress }{_apiUrl.VilaDetailAddress}/GetAllVilaDetails/{id}";
            var model = await _detail.GetAll(url, secret);
            ViewData["vila"] = vila;
            return View(model);
        }

        public IActionResult CreateVilaDetail(int id)
        {
            DetailModel model = new() { VilaId = id };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVilaDetail(int id,DetailModel model)
        {
            if (id != model.VilaId) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var secret = _auth.GetJwtToken();
            var url = $"{_apiUrl.BaseAddress }{_apiUrl.VilaDetailAddress}";

            bool create = await _detail.Create(url, secret, model);


            if (create)
                TempData["success"] = true;

            return Redirect($"/Admin/GetVilaDetails/{id}");
        }

        public async Task<IActionResult> EditVilaDetail(int id)
        {
            var secret = _auth.GetJwtToken();
            var url = $"{_apiUrl.BaseAddress }{_apiUrl.VilaDetailAddress}/GetById/{id}";
            var model = await _detail.GetById(url, secret);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditVilaDetail(int id,DetailModel model)
        {
            if (model.DetailId != id) return BadRequest();
            var secret = _auth.GetJwtToken();
            var url = $"{_apiUrl.BaseAddress }{_apiUrl.VilaDetailAddress}/{id}";

            bool update = await _detail.Update(url, secret, model);


            if (update)
                TempData["success"] = true;

            return Redirect($"/Admin/GetVilaDetails/{model.VilaId}");

        }

        public async Task<IActionResult> DeleteVilaDetail(int id)
        {
            var secret = _auth.GetJwtToken();
            var urlget = $"{_apiUrl.BaseAddress }{_apiUrl.VilaDetailAddress}/GetById/{id}";
            var model = await _detail.GetById(urlget, secret);

            var url = $"{_apiUrl.BaseAddress }{_apiUrl.VilaDetailAddress}/{id}";

            bool delete = await _detail.Delete(url, secret);


            if (delete)
                TempData["success"] = true;

            return Redirect($"/Admin/GetVilaDetails/{model.VilaId}");
        }
        #endregion
    }
}
