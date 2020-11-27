using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using News.Contracts.V1.Responses;
using News.Data;
using News.Domain;

namespace News.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        private readonly IGroupService _groupService;

        public IdentityService(UserManager<User> userManager, DataContext context, RoleManager<IdentityRole> roleManager, IGroupService groupService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _groupService = groupService;
        }

        public async Task<AuthSuccessResponse> RegisterAsync(string email, string name, string password, string role, string group, List<Subject> subjects)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthSuccessResponse
                {
                    Success = false,
                    Errors = new[] { "email занят" }
                };
            }

            var newUserId = Guid.NewGuid();
            var newUser = new User
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = name,
                subjects = subjects,
                group = null
            };

            if (group != null)
                newUser.group = await _groupService.GetGroupAsync(group);

            if (!_roleManager.Roles.Select(x => x.Name == role).Any())
                return new AuthSuccessResponse
                {
                    Success = false,
                    Errors = new[] { "Такой роли не существует" }
                };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthSuccessResponse
                {
                    Success = false,
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            await _userManager.AddToRoleAsync(newUser, role);
            await _context.SaveChangesAsync();

            return new AuthSuccessResponse()
            {
                Id = newUser.Id,
                Email = newUser.Email,
                Name = newUser.UserName,
                Group = group,
                subjects = subjects,
                Success = true
            };
        }

        public async Task<AuthSuccessResponse> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthSuccessResponse
                {
                    Success = false,
                    Errors = new[] { "User does not exist" }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthSuccessResponse
                {
                    Success = false,
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return new AuthSuccessResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName ??= null,
                Group = (user.group != null) ? user.group.Id : null,
                subjects = (user.subjects != null) ? user.subjects : null,
                Roles = (userRoles != null) ? userRoles.ToList() : null,
                Success = true
        };
        }

        public async Task<User> GetUsersByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }
        public async Task<User> GetUserByEmail(string name)
        {
            return await _userManager.FindByEmailAsync(name);
        }

        //private ClaimsPrincipal GetPrincipalFromToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {
        //        var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
        //        if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
        //        {
        //            return null;
        //        }

        //        return principal;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        //{
        //    var validatedToken = GetPrincipalFromToken(token);

        //    if (validatedToken == null)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
        //    }

        //    var expiryDateUnix =
        //        long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        //    var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        //        .AddSeconds(expiryDateUnix);

        //    if (expiryDateTimeUtc > DateTime.UtcNow)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
        //    }

        //    var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //    var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

        //    if (storedRefreshToken == null)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
        //    }

        //    if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
        //    }

        //    if (storedRefreshToken.Invalidated)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };
        //    }

        //    if (storedRefreshToken.Used)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
        //    }

        //    if (storedRefreshToken.JwtId != jti)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
        //    }

        //    storedRefreshToken.Used = true;
        //    _context.RefreshTokens.Update(storedRefreshToken);
        //    await _context.SaveChangesAsync();

        //    var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
        //    return await GenerateAuthenticationResultForUserAsync(user);
        //}

        //private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        //{
        //    return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
        //           jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
        //               StringComparison.InvariantCultureIgnoreCase);
        //}

        //private async Task<AuthSuccessResponse> GenerateAuthenticationResultForUserAsync(User user)
        //{
        //    #region
        //    //var tokenHandler = new JwtSecurityTokenHandler();
        //    //var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        //    //var claims = new List<Claim>
        //    //{
        //    //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //    //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    //    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
        //    //    new Claim("id", user.Id)
        //    //};

        //    //var userClaims = await _userManager.GetClaimsAsync(user);
        //    //claims.AddRange(userClaims);
        //    #endregion
        //    var userRoles = await _userManager.GetRolesAsync(user);
        //    #region
        //    //foreach (var userRole in userRoles)
        //    //{
        //    //    claims.Add(new Claim(ClaimTypes.Role, userRole));
        //    //    var role = await _roleManager.FindByNameAsync(userRole);
        //    //    if (role == null) continue;
        //    //    var roleClaims = await _roleManager.GetClaimsAsync(role);

        //    //    foreach (var roleClaim in roleClaims)
        //    //    {
        //    //        if (claims.Contains(roleClaim))
        //    //            continue;

        //    //        claims.Add(roleClaim);
        //    //    }
        //    //}

        //    //var tokenDescriptor = new SecurityTokenDescriptor
        //    //{
        //    //    Subject = new ClaimsIdentity(claims),
        //    //    Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
        //    //    SigningCredentials =
        //    //        new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    //};

        //    //var token = tokenHandler.CreateToken(tokenDescriptor);

        //    //await _context.SaveChangesAsync();
        //    //var roles = await _userManager.GetRolesAsync(user);
        //    //string business = null;
        //    //try
        //    //{
        //    //    var userBusiness = await _identityService.Ge.(user.Id);
        //    //    business = userBusiness.Name;
        //    //}
        //    //catch (Exception ex) { Console.WriteLine("User doesn't have business"); }
        //    #endregion
        //    return new AuthSuccessResponse
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        Roles = userRoles.ToList<string>(),
        //        subjects = user.subjects
        //    };
        //}

        public async Task<IdentityResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            try
            {
                if ((await _userManager.GetRolesAsync(user)).First().ToLower() == "admin")
                {
                    if ((await _userManager.GetUsersInRoleAsync("Admin")).Count <= 1)
                    {
                        return IdentityResult.Failed(new IdentityError()
                        {
                            Code = "1",
                            Description = "You can't delete the only administrator"
                        });
                        // result.Errors.Append(new IdentityError()
                        // {
                        //     Code = "1",
                        //     Description = "You can't delete the only administrator"
                        // });
                        // return result;
                    }
                }
            }
            catch (Exception ex) { }

            return await _userManager.DeleteAsync(user);
        }
    }
}