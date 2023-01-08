using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Customer;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customer;
        public CustomerController(ICustomerService customer)
        {
            _customer = customer;
        }
        /// <summary>
        /// ثبت نام
        /// </summary>
        /// <param name="model"> موبایل و کلمه عبور</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(_customer.ExistMobile(model.Mobile))
            {
                return BadRequest(new {error = "شماره موبایل تکراری است ." });
            }
            if(_customer.Register(model))
            {
                return StatusCode(201);
            }
            else
            {
                ModelState.AddModelError("", "خطای سیستمی ... لطفا مجددا اقدام نمایید !!!");
                return StatusCode(500, ModelState);
            }
        }
        /// <summary>
        /// ورود به سایت
        /// </summary>
        /// <param name="login"> موبایل و کلمه عبور</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(200,Type =typeof(LoginResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody] RegisterModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customer.PasswordIsCorrect(login.Mobile,login.Pass))
            {
                ModelState.AddModelError("model.Mobile", "کاربری یافت نشد .");
                return BadRequest(ModelState);
            }
            var user = _customer.Login(login.Mobile, login.Pass);
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
