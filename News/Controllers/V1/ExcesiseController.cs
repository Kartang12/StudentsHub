using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Controllers.V1
{
    public class ExcesiseController : Controller
    {
        private readonly IExerciseService _excersiseService;

        public ExcesiseController(IExerciseService es)
        {
            _excersiseService = es;
        }

        [HttpGet(ApiRoutes.Excersises.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _excersiseService.GetAll());
        }
        
        [HttpGet(ApiRoutes.Excersises.Get)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok(await _excersiseService.GetExerciseByIdAsync(id));
        }

        [HttpGet(ApiRoutes.Excersises.GetBySubject)]
        public async Task<IActionResult> GetBySubject([FromRoute] string subjectId)
        {
            var ex = await _excersiseService.GetExercisesBySubjectAsync(subjectId);

            if (ex == null)
            {
                return NotFound();
            }

            return Ok(ex);
        }

        [HttpPost(ApiRoutes.Excersises.Save)]
        public async Task<IActionResult> SaveExcersiseAsync([FromBody] ExerciseAnswerRequest request)
        {
            var ex = await _excersiseService.SaveExcersiseAsync(request.userId, request.taskId, request.answer);

            if (ex == false)
            {
                return NotFound();
            }

            return Ok(ex);
        }

        [HttpPost(ApiRoutes.Excersises.Create)]
        public async Task<IActionResult> Create([FromBody] CreateEcerciseRequest request)
        {
            var created = await _excersiseService.CreateExcersiseAsync(request.Title, request.Content, request.CorrectAnswer, request.SubjectId);
            if (!created)
            {
                return BadRequest(new { error = "Unable to create excersise" });
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Excersises.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deleted = await _excersiseService.DeleteExcesiseAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpPut(ApiRoutes.Excersises.Update)]
        public async Task<IActionResult> Update([FromBody] ExerciseUpdateRequest ex)
        {
            var updated = await _excersiseService.UpdateExcersiseAsync(ex.Id, ex.Title, ex.Content, ex.CorrectAnswer);

            if (updated)
                return Ok();

            return NotFound();
        }

        [HttpGet(ApiRoutes.Excersises.GetMarks)]
        public async Task<IActionResult> GetMarks([FromRoute] string id)
        {
            var marks = await _excersiseService.GetMarksAsync(id);

            if (marks!=null)
                return Ok(marks);

            return NotFound();
        }
    }
}
