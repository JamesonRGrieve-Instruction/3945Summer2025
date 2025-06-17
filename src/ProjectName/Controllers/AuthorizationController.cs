using System.Security.Cryptography;
using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
namespace ProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        string username = "Test";
        string password = "Password1";
        [HttpPost("")]
        public ActionResult Authorize()
        {
            string authHeader = Request.Headers.Authorization.ToString();
            if (string.IsNullOrWhiteSpace(authHeader)) return StatusCode(401, "No credentials provided.");
            if (authHeader.Split(" ")[0] != "Basic") return StatusCode(400, "Authorization with Basic is the only type supported.");
            try
            {
                string[] decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Split(' ')[1])).Split(':');
                if (decoded[0] != username || decoded[1] != password) throw new Exception();
            }
            catch
            {
                return StatusCode(401, "Invalid credentials.");
            }
            var jwt = JwtBuilder.Create().WithAlgorithm(new HMACSHA256Algorithm()).WithSecret("Test").AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()).AddClaim("test", 42).Encode();
            Response.Headers.SetCookie = $"jwt={jwt};";
            return NoContent();
        }
    }
}