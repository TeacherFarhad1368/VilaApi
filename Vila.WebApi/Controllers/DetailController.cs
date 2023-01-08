using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Detail;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/detail")]
    //[ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class DetailController : ControllerBase
    {
        private readonly IDetailService _detail;
        private readonly IVilaService _vila;
        private readonly IMapper _mapper;
        public DetailController(IDetailService detail,IMapper mapper,IVilaService vila)
        {
            _detail = detail;
            _mapper = mapper;
            _vila = vila;
        }
        /// <summary>
        /// دریافت تمام جزییات یک ویلا
        /// </summary>
        /// <param name="vilaId">کلید ویلا</param>
        /// <returns></returns>
        [HttpGet("[action]/{vilaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(List<DetailDto>))]
        public IActionResult GetAllVilaDetails(int vilaId)
        {
            var vila = _vila.GetById(vilaId);
            if (vila == null)
                return NotFound();

            var details = _detail.GetAllVilaDetails(vilaId);
            List<DetailDto> model = new();
            details.ForEach(x =>
            {
                model.Add(_mapper.Map<DetailDto>(x));
            });
            return Ok(model);
        }
        /// <summary>
        /// دریافت یک جز ویلا
        /// </summary>
        /// <param name="detailId">ای دی جز </param>
        /// <returns></returns>
        [HttpGet("[action]/{detailId:int}" ,Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailDto))]
        [ProducesResponseType(404)]
        public IActionResult GetById(int detailId)
        {
            var detail = _detail.GetById(detailId);
            if (detail == null) return StatusCode(404);
            var model = _mapper.Map<DetailDto>(detail);
            return StatusCode(200, model);
        }
        /// <summary>
        /// افزودن جزییات به ویلا
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] DetailDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detail = _mapper.Map<Models.Detail>(model);
            if (_detail.Create(detail))
            {
                var dtoDetail = _mapper.Map<DetailDto>(detail);
                return CreatedAtRoute("GetById",new { detailId  = dtoDetail.DetailId}, dtoDetail);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
        /// <summary>
        /// ویرایش جزییات ویلا
        /// </summary>
        /// <param name="detailId">کلید جز</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{detailId:int}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int detailId, [FromBody] DetailDto model)
        {
            if (detailId != model.DetailId)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detail = _mapper.Map<Models.Detail>(model);
            if (_detail.Update(detail))
            {
                //return NoContent();
                return StatusCode(204);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
        /// <summary>
        /// حذف جز ویلا
        /// </summary>
        /// <param name="detailId">کلید جز</param>
        /// <returns></returns>
        [HttpDelete("{detailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Remove(int detailId)
        {
            var detail = _detail.GetById(detailId);
            if (detail == null)
                return NotFound();

            if (_detail.Delete(detail))
            {
                return NoContent();
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
    }
}
