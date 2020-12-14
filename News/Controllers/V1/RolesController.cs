using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Contracts.V1;
using News.Contracts.V1.Requests;

namespace News.Controllers.V1
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet(ApiRoutes.Roles.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_roleManager.Roles);
        }
        
        [HttpPost(ApiRoutes.Roles.Add)]
        public async Task<IActionResult> Add([FromBody] RoleRequest role)
        {
            return Ok(await _roleManager.CreateAsync(new IdentityRole(role.RoleName)));
        }


        [HttpDelete(ApiRoutes.Roles.Delete)]
        public async Task<IActionResult> Delete([FromBody] RoleRequest role)
        {
            return Ok(await _roleManager.DeleteAsync(new IdentityRole(role.RoleName)));
        }
    }
}