using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<UserDto> GetUsers()
        {
            return _context.Users
                .Include(x => x.Tasks)
                .Select(x => new UserDto
                {
                    Username = x.Username,
                    Email = x.Email,
                    Id = x.Id,
                    Tasks = x.Tasks
                        .Select(task => new TaskDto
                        { 
                            Id = task.Id,
                            Description = task.Description,
                            Name = task.Name
                        })
                });
        }

        [HttpPost]
        public long CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(long id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return StatusCode(404);
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return StatusCode(200);

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult TaskOne(long id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return StatusCode(404);
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public long EditTask(long id, [FromBody] User newUser)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
            _context.Users.Add(newUser);
            newUser.Id = user.Id;
            _context.SaveChanges();
            return newUser.Id;
        }
    }
}
