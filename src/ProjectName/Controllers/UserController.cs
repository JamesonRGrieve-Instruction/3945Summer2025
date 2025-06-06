using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Models;
namespace ProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public static List<User> users = new List<User>();
        [HttpPost("")]
        public ActionResult<User> CreateUser(string email, string userName)
        {
            User user = new User()
            {
                EMail = email,
                UserName = userName
            };
            users.Add(user);
            return user;
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(string id)
        {
            return users.Find(user => user.ID.ToString() == id);
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<User>> ListUsers()
        {
            return users;
        }
        [HttpPut("{id}")]
        public ActionResult<User> PutUser(string id, string email, string userName)
        {
            User target = users.Find(user => user.ID.ToString() == id);
            target.EMail = email;
            target.UserName = userName;
            return target;
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
        {
            users.Remove(users.Find(user => user.ID.ToString() == id));
            return NoContent();
        }
        [HttpPost("{id}/post")]
        public ActionResult<Post> CreateUserPost(string title, string content)
        {
            // Rather that duplicating the logic, we could keep it DRYer and forward our creation to the PostController inferring the user ID from our path.
            // FIXME Implement this.
            return RedirectToAction("CreatePost", "PostController");
        }
    }
}