using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Contracts.V1.Responses;
using News.Domain;
using News.Services;

namespace News.Controllers.V1
{
    public class UsersController : Controller
    {
        //private readonly IIdentityService _identityService;
        //private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        //public UsersController(UserManager<User> userManager, IIdentityService identityService, RoleManager<IdentityRole> roleManager)
        //{
        //    _userManager = userManager;
        //    _identityService = identityService;
        //    _roleManager = roleManager;
        //}
            
        //[HttpGet(ApiRoutes.Users.GetAll)]
        //public async Task<IActionResult> GetAll()
        //{
        //    var rawUsers = _userManager.Users;
        //    List<UserDataResponse> response = new List<UserDataResponse>();
        //    foreach (IdentityUser user in rawUsers)
        //    {
        //        string role = null;
        //        try
        //        {
        //            role = (await _userManager.GetRolesAsync(user)).First();
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        response.Add(new UserDataResponse()
        //        {
        //            Name = user.UserName,
        //            BusinessType = role
        //        });
        //    }
        //    return Ok(response);
        //}
        
        //[HttpGet(ApiRoutes.Users.Get)]
        //public async Task<IActionResult> Get([FromRoute] string userName)
        //{
        //    var user = await _identityService.GetUserByName(userName);
        //    string role = null;
        //    try
        //    {
        //        role = (await _userManager.GetRolesAsync(user)).First();
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return Ok(new UserDataResponse()
        //    {
        //        Name = user.UserName,
        //        BusinessType = role
        //    });
        //    // return Ok(await _identityService.GetUserByName(userName));
        //}
        
        //[HttpPost(ApiRoutes.Users.Add)]
        //public async Task<IActionResult> Add([FromBody] UserRegistrationRequest request)
        //{
        //    var registered = await _identityService.RegisterAsync(request.Email, request.Name, request.Password, request.Role, request.Business);
            
        //    if(registered.Errors == null)
        //        return Ok();

        //    return BadRequest(registered.Errors);
        //}        
        
        //[HttpDelete(ApiRoutes.Users.Delete)]
        //public async Task<IActionResult> Delete([FromRoute] string userName)
        //{
        //    var result = await _identityService.DeleteUser(userName);
            
        //    if(result.Succeeded)
        //        return Ok();

        //    if(result.Errors.First().Code == "1")
        //        return BadRequest(new {error = "You can't delete the last administrator"});
            
        //    return BadRequest(new {error = "Unable to delete user"});
        //}

        //[HttpPut(ApiRoutes.Users.Update)]
        //public async Task<IActionResult> Update([FromRoute] string userName, [FromBody] UserUpdateRequest request)
        //{
        //    IdentityUser nameIsTaken = null;
        //    if(userName != request.Name)
        //        nameIsTaken = await _identityService.GetUserByName(request.Name);
            
        //    if(nameIsTaken != null)
        //        return BadRequest(new {error = "This username is taken"});
            
        //    var user = await _identityService.GetUserByName(userName);
            
        //    user.UserName = request.Name;
        //    user.Email = request.Name;

        //    if(request.Role.Length > 0 )
        //    {
        //        try
        //        {
        //            var currentRole = (await _userManager.GetRolesAsync(user)).First();
        //            await _userManager.RemoveFromRoleAsync(user, currentRole);
        //        }
        //        catch (Exception e) { }
        //        await _userManager.AddToRoleAsync(user, request.Role);
        //    }

        //    return Ok();
        //}

        //[HttpPut(ApiRoutes.Users.Change)]
        //public async Task<IActionResult> Change([FromRoute] string userName, [FromBody] UserSelfUpdateRequest request)
        //{
        //    var user = await _identityService.GetUserByName(userName);

        //    user.UserName = request.Name;
        //    user.Email = request.Name;
            
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    await _userManager.ResetPasswordAsync(user, token, request.Password);

        //    return Ok();
        //}

    }
}