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
        private readonly IExcersiseService _excersiseService;

        public ExcesiseController(IExcersiseService es)
        {
            _excersiseService = es;
        }

        [HttpGet(ApiRoutes.Excersises.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _excersiseService.GetAll());
        }
        
        [HttpGet(ApiRoutes.Excersises.GetById)]
        public async Task<IActionResult> GetAll([FromRoute] string id)
        {
            return Ok(await _excersiseService.GetExcerseByIdAsync(id));
        }

        [HttpGet(ApiRoutes.Excersises.Get)]
        public async Task<IActionResult> GetBySubject([FromRoute] string subjectName)
        {
            var ex = await _excersiseService.GetExcersesBySubjectAsync(subjectName);

            if (ex == null)
            {
                return NotFound();
            }

            return Ok(ex);
        }

        [HttpPost(ApiRoutes.Excersises.Create)]
        public async Task<IActionResult> Create([FromBody] CreateExcersiseRequest request)
        {
            var created = await _excersiseService.CreateExcersiseAsync(request.Title, request.Content, request.CorrectAnswer, request.SubjectName);
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
    }
}
