using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Data;
using News.Domain;
using News.Services;
using System.Threading.Tasks;

namespace News.Controllers.V1
{
    public class FormsController : Controller
    {
        private readonly IFormService _formService;

        public FormsController(IFormService gs)
        {
            _formService = gs;
        }

        [HttpGet(ApiRoutes.Forms.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _formService.GetAllFormsAsync());
        }

        [HttpGet(ApiRoutes.Forms.Get)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var subject = await _formService.GetFormAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        [HttpPost(ApiRoutes.Forms.Create)]
        public async Task<IActionResult> Create([FromBody] FormRequest request)
        {
            var created = await _formService.CreateFormAsync(request.Number);
            if (!created)
            {
                return BadRequest(new { error = "Unable to create tag" });
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Forms.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deleted = await _formService.DeleteFormAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }

}
