using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<AppRole> _roleManager;
        public LoginController(IConfiguration config, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetLogin()
        {
            var c = User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();
            if (c != null)
            {
                return Ok(c.Value);
            }
            return NotFound();
        }

        //[Authorize]
        //[HttpGet("claims")]
        //public object Claims()
        //{
        //    return User.Claims.Select(c =>
        //    new
        //    {
        //        Type = c.Type,
        //        Value = c.Value
        //    });
        //}

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserIM userData)
        {
            Console.Write(userData.ToString());

            var result = await _signInManager.PasswordSignInAsync(userData.Name, userData.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userData.Name);
                string token = GenerateJSONWebToken(user);
                return Ok(token);
            }
            return Forbid();
        }

        private string GenerateJSONWebToken(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(type:ClaimTypes.Role, _roleManager.FindByNameAsync(user.UserName).Result.ToString())
            };

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, _roleManager.FindByNameAsync(user.UserName).Result.ToString()));
            //User.AddIdentity(claimsIdentity);

            var expiration = DateTime.Now.AddMinutes(10);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;           
        }
    }
}
