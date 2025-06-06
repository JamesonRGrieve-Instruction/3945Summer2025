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
        [HttpGet("all")]
        public ActionResult<IEnumerable<string>> GetAllPeople()
        {
            return new string[] {
                "John",
                "Joe",
                "Bob",
                "Sue"
            };
        }
        [HttpPatch("")]
        public ActionResult<IEnumerable<string>> PatchPeople()
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