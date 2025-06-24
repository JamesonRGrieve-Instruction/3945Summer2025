using Microsoft.AspNetCore.Mvc;
using YourApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Http.HttpResults;


namespace YourApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = _signInManager;
            _configuration = configuration;
        }
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] CredentialRequest request)
        {
            if (request == null)
            {
                return BadRequest("No credentials provided.");
            }
            if ((await _userManager.CreateAsync(new IdentityUser { Email = request.Email }, request.Password)).Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Invalid registration request.");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CredentialRequest request)
        {
            if (request == null)
            {
                return BadRequest("No credentials provided.");
            }
            IdentityUser? user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return BadRequest("Invalid credentials provided.");
            }
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
           );
            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });

        }

    }

    public class CredentialRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}