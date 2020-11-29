using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Services;

namespace News.Controllers.V1
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet(ApiRoutes.Subjects.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _subjectService.GetSubjectsAsync());
        }

        [HttpGet(ApiRoutes.Subjects.Get)]
        public async Task<IActionResult> Get([FromRoute] string subName)
        {
            var subject = await _subjectService.GetAsync(subName);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }
        
        [HttpGet(ApiRoutes.Subjects.GetByUser)]
        public async Task<IActionResult> GetSubjectsByUserId([FromRoute] string userId)
        {
            var subject = await _subjectService.GetSubjectsByUserIdAsync(userId);

            if (subject == null)
            {return NotFound();}

            return Ok(subject);
        }

        [HttpPost(ApiRoutes.Subjects.Create)]
        public async Task<IActionResult> Create([FromBody] CreateSubjectRequest request)
        {
            var created = await _subjectService.CreateSubjectAsync(request.Name);
            if (!created)
            {
                return BadRequest(new { error = "Subject exists" });
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Subjects.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string subName)
        {
            var deleted = await _subjectService.DeleteSubjectAsync(subName);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}