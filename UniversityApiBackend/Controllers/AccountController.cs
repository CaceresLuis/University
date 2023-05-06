using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDBContex _contex;
        private readonly IStringLocalizer<AccountController> _stringLocalizer;

        public AccountController(JwtSettings jwtSettings, UniversityDBContex contex, IStringLocalizer<AccountController> stringLocalizer)
        {
            _contex = contex;
            _jwtSettings = jwtSettings;
            _stringLocalizer = stringLocalizer;
        }

        private async Task<List<User>> GetUser()
        {
            return await _contex.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogins)
        {
            try
            {
                List<User> Logins = await GetUser();
                UserTokens Token = new();
                User? searchUser = (from user in _contex.Users
                                    where user.Email == userLogins.UserName && user.Password == userLogins.Password
                                    select user).FirstOrDefault();

                if (searchUser != null)
                {
                    LocalizedString messege = _stringLocalizer["Welcome"];
                    Token = JwHelpers.GetTokenKey(new UserTokens()
                    {
                        UserName = $"{messege}: {searchUser.Name}",
                        Role = searchUser.Role,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrond password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {

                throw new Exception("GetToken error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(await GetUser());
        }
    }
}
