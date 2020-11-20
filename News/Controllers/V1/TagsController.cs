using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Domain;
using News.Extensions;
using News.Services;

namespace News.Controllers.V1
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        //private readonly GroupService _tagService;

        //public TagsController(GroupService tagService)
        //{
        //    _tagService = tagService;
        //}

        //[HttpGet(ApiRoutes.Tags.GetAll)]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _tagService.GetAllTagsAsync());
        //}
        
        //[HttpGet(ApiRoutes.Tags.Get)]
        //public async Task<IActionResult> Get([FromRoute]string tagName)
        //{
        //    var tag = await _tagService.GetTagByNameAsync(tagName);

        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tag);
        //}

        //[HttpPost(ApiRoutes.Tags.Create)]
        //public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        //{
        //    var created = await _tagService.CreateTagAsync(request.TagName);
        //    if (!created)
        //    {
        //        return BadRequest(new {error = "Unable to create tag"});
        //    }

        //    //var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        //    //var locationUri = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", newTag.Name);
        //    return Ok();
        //    //return Created(locationUri, newTag);
        //}
        
        //[HttpDelete(ApiRoutes.Tags.Delete)]
        //public async Task<IActionResult> Delete([FromRoute] string tagName)
        //{
        //    var deleted = await _tagService.DeleteTagAsync(tagName);

        //    if (deleted)
        //        return NoContent();
            
        //    return NotFound();
        //}
    }
}