using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Vila;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Vila.WebApi.Controllers
{
    //[Route("api/vila")]
    [Route("api/v{version:apiVersion}/Vila")]
    //[ApiVersion("1.0")]
    [ApiController]
    public class VilaController : ControllerBase
    {
        private readonly IVilaService _vila;
        private readonly IMapper _mapper;
        public VilaController(IVilaService vila, IMapper mapper)
        {
            _vila = vila;
            _mapper = mapper;
        }
        /// <summary>
        /// دریافت لیست تمام ویلا ها
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VilaDto>))]
        public IActionResult GetAll()
        {
            var list = _vila.GetAll();
            List<VilaDto> model = new();

            list.ForEach(x =>
            {
                model.Add(_mapper.Map<VilaDto>(x));
            });

            return Ok(model);
        }
        /// <summary>
        /// دریافت یک ویلا به آی دی ویلا
        /// </summary>
        /// <param name="vilaId">آی دی ویلا</param>
        /// <returns></returns>
        [HttpGet("[action]/{vilaId:int}", Name = "GetDetails")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VilaDto))]
        [ProducesResponseType(404)]
        public IActionResult GetDetails([FromRoute] int vilaId)
        {
            var vila = _vila.GetById(vilaId);
            if (vila == null) return NotFound();
            var model = _mapper.Map<VilaDto>(vila);
            return Ok(model);
        }
        //[HttpGet("[action]")]
        //public IActionResult GetVilaAddress([FromHeader] int vilaId)
        //{
        //    var vila = _vila.GetById(vilaId);
        //    if (vila == null) return NotFound();
        //    return Ok(new { id = vila.VilaId, state = vila.State, city = vila.City, address = vila.address });
        //}
        //[HttpGet("[action]")]
        //public IActionResult GetVilaMobile([FromQuery] int vilaId)
        //{
        //    var vila = _vila.GetById(vilaId);
        //    if (vila == null) return NotFound();
        //    return Ok(new { id = vila.VilaId, mobile = vila.Mobile });
        //}
        //[HttpPost]
        //public IActionResult Create([FromForm] VilaDto model)
        //{
        //    var vila =  _mapper.Map<Models.Vila>(model);
        //    _vila.Create(vila);
        //    return StatusCode(201);
        //}
        /// <summary>
        /// ایجاد یک ویلای جدید 
        /// </summary>
        /// <param name="model"> اطلاعات ویلا (VilaDto)</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        //[MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VilaDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] VilaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vila = _mapper.Map<Models.Vila>(model);
            if (_vila.Create(vila))
            {
                return CreatedAtRoute("GetDetails", new { vilaId = vila.VilaId }, _mapper.Map<VilaDto>(vila));
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
        /// <summary>
        /// ویرایش ویلا
        /// </summary>
        /// <param name="vilaId">آی دی ویلا</param>
        /// <param name="model"> اطلاعات ویلا (VilaDto)</param>
        /// <returns></returns>
        [HttpPatch("{vilaId:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int vilaId, [FromBody] VilaDto model)
        {
            if (vilaId != model.VilaId)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vila = _mapper.Map<Models.Vila>(model);
            if (_vila.Update(vila))
            {
                //return NoContent();
                return StatusCode(204);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
        /// <summary>
        /// حذف ویلا
        /// </summary>
        /// <param name="vilaId">کلید ویلا</param>
        /// <returns></returns>
        [HttpDelete("{vilaId:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Remove(int vilaId)
        {
            var vila = _vila.GetById(vilaId);
            if (vila == null)
                return NotFound();

            if (_vila.Delete(vila))
            {
                return NoContent();
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
    }
}
