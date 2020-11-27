using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Data;
using News.Domain;
using News.Services;
using System.Threading.Tasks;

namespace News.Controllers.V1
{
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService gs)
        {
            _groupService = gs;
        }

        [HttpGet(ApiRoutes.Groups.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _groupService.GetAllGroupsAsync());
        }

        [HttpGet(ApiRoutes.Groups.Get)]
        public async Task<IActionResult> Get([FromRoute] string groupName)
        {
            var subject = await _groupService.GetGroupAsync(groupName);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        [HttpPost(ApiRoutes.Groups.Create)]
        public async Task<IActionResult> Create([FromBody] GroupRequest request)
        {
            var created = await _groupService.CreateGroupAsync(request.Name);
            if (!created)
            {
                return BadRequest(new { error = "Unable to create tag" });
            }

            return Ok();
        }

        [HttpDelete(ApiRoutes.Groups.Delete)]
        public async Task<IActionResult> Delete([FromBody] GroupRequest request)
        {
            var deleted = await _groupService.DeleteGroupAsync(request.Name);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }

}
