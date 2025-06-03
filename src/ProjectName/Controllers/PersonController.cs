using Microsoft.AspNetCore.Mvc;
namespace ProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<IEnumerable<string>> GetPeople()
        {
            return new string[] {
                "John",
                "Joe",
                "Bob",
                "Sue"
            };
        }
    }
}