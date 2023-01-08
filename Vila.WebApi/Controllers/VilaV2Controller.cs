using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Paging;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/vila")]
    [Authorize]
    //[Route("api/vila")]
    [ApiVersion("2.0")]
    [ApiController]
    public class VilaV2Controller : ControllerBase
    {
        private readonly IVilaService _vila;
        public VilaV2Controller(IVilaService vila)
        {
            _vila = vila;
        }
        /// <summary>
        /// جست وجوی ویلا
        /// </summary>
        /// <param name="pageId">صفحه چندم ؟</param>
        /// <param name="filter">متن جست وجو</param>
        /// <param name="take">تعداد نمایش</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type =typeof(VilaPaging))]
        [ProducesResponseType(400)]
        public IActionResult Search(int pageId = 1,string? filter = "",int take = 2)
        {
            if (pageId < 1 || take < 1) return BadRequest();
            var model = _vila.SearchVila(pageId, filter, take);
            return Ok(model);
        }
    }
}
