using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Stores;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers() => Ok(UserStore.Users);

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = UserStore.Users.FirstOrDefault(u => u.Id == id);
            return user == null ? NotFound(new { error = "User not found" }) : Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            user.Id = UserStore.Users.Count + 1;
            UserStore.Users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = UserStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { error = "User not found" });

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = UserStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { error = "User not found" });

            UserStore.Users.Remove(user);
            return NoContent();
        }
    }
}
