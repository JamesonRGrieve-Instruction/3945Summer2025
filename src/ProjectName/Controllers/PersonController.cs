using Microsoft.AspNetCore.Mvc;
namespace ProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        static string[] People = new string[] {
            "Liam",
            "Olivia",
            "Noah",
            "Emma",
            "Oliver",
            "Ava",
            "Elijah",
            "Sophia",
            "James",
            "Isabella",
            "William",
            "Mia",
            "Benjamin",
            "Charlotte",
            "Lucas",
            "Amelia",
            "Henry",
            "Harper",
            "Alexander",
            "Evelyn",
            "Daniel",
            "Abigail",
            "Matthew",
            "Emily",
            "Sebastian",
            "Ella",
            "Jack",
            "Elizabeth",
            "Samuel",
            "Camila",
            "David",
            "Luna",
            "Joseph",
            "Sofia",
            "Carter",
            "Avery",
            "Owen",
            "Scarlett",
            "Wyatt",
            "Grace",
            "John",
            "Chloe",
            "Leo",
            "Victoria",
            "Luke",
            "Riley",
            "Julian",
            "Aria",
            "Grayson",
            "Lily"
        };
        [HttpGet("")]
        public ActionResult<IEnumerable<string>> GetPeople(int page, int pageSize, bool reverse)
        {
            string[] sortedPeople = (reverse ? People.OrderByDescending(x => x) : People.OrderBy(x => x)).ToArray();
            return sortedPeople.Skip((page - 1) * pageSize).Take(pageSize).ToArray();
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<string>> GetAllPeople()
        {
            return People;
        }
        [HttpGet("{name}")]
        public ActionResult<int> GetListPosition(string name)
        {
            return 1 + Array.IndexOf(People.OrderBy(x => x).ToArray(), name);
        }
    }
}