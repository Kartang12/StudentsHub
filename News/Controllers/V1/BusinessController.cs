using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Data;
using News.Domain;
using News.Services;
using System.Threading.Tasks;

namespace News.Controllers.V1
{
    public class BusinessController : Controller
    {
        //private readonly IBusinessService _businessService;

        //public BusinessController(IBusinessService bs)
        //{
        //    _businessService = bs;
        //}

        //[HttpGet(ApiRoutes.Businesses.GetAll)]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _businessService.GetAllBusinesseAsync());
        //}

        //[HttpPost(ApiRoutes.Businesses.Add)]
        //public async Task<IActionResult> Add([FromBody] CreateBusinessRequest businessType)
        //{
        //    return Ok(await _businessService.CreateBusinessAsync(businessType.Name));
        //}

        //[HttpDelete(ApiRoutes.Businesses.Delete)]
        //public async Task<IActionResult> Delete([FromBody] CreateBusinessRequest businessType)
        //{
        //    return Ok(await _businessService.DeleteBusinessAsync(businessType.Name));
        //}
    }

}
