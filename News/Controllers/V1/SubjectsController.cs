using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Domain;
using News.Services;

namespace News.Controllers.V1
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly IExerciseService _exerciseService;
        public SubjectsController(ISubjectService subjectService, IExerciseService exerciseService)
        {
            _subjectService = subjectService;
            _exerciseService = exerciseService;
        }

        [HttpGet(ApiRoutes.Subjects.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _subjectService.GetSubjectsAsync());
        }

        [HttpGet(ApiRoutes.Subjects.Get)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var subject = await _subjectService.GetAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }
        
        [HttpGet(ApiRoutes.Subjects.ForTeatchers)]
        public async Task<IActionResult> GetSubjectsForTeatchers([FromRoute] string id)
        {
            var subject = await _subjectService.GetSubjectsByUserIdAsync(id);
            foreach(var sub in subject) 
            {
                sub.exercises = await _exerciseService.GetExercisesBySubjectAsync(sub.Id.ToString());
            }
            if (subject == null)
                {return NotFound();}

            return Ok(subject);
        }
          
        [HttpGet(ApiRoutes.Subjects.ForStudent)]
        public async Task<IActionResult> GetSubjectsForStudent([FromRoute] string id)
        {
            var subject = await _subjectService.GetSubjectsForStudent(id);
            foreach (var sub in subject)
            {
                sub.exercises = await _exerciseService.GetExercisesBySubjectAsync(sub.Id.ToString());
            }
            if (subject == null)
                {return NotFound();}

            return Ok(subject);
        }

        [HttpPost(ApiRoutes.Subjects.Create)]
        public async Task<IActionResult> Create([FromBody] CreateSubjectRequest request)
        {
            var created = await _subjectService.CreateSubjectAsync(request.Name, request.FormId);
            if (!created)
            {
                return BadRequest(new { error = "Subject exists" });
            }

            return Ok();
        }

        [HttpPut(ApiRoutes.Subjects.Update)]
        public async Task<IActionResult> Update([FromBody] CreateSubjectRequest request)
        {
            var created = await _subjectService.UpdateSubjectAsync(request.Id, request.Name, request.FormId);
            if (!created)
            {
                return BadRequest(new { error = "Subject exists" });
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Subjects.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deleted = await _subjectService.DeleteSubjectAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}