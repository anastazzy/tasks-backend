using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TaskController(MyDbContext context)  
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<TaskDto> GetTasks()
        {
            return _context.Tasks
                .Include(x => x.User)
                .Select(x => new TaskDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    User = new UserDto
                    {
                        Id = x.User.Id,
                        Username = x.User.Username,
                        Email = x.User.Email,
                    }
                });

        }

        [HttpPost]
        public long CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task.Id;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTask(long id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return StatusCode(404);
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return StatusCode(200);

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult TaskOne(long id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return StatusCode(404);
            } else
            {
                return Ok(task);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public long EditTask(long id,  [FromBody]Task newTask)
        {
            var oldTask = _context.Tasks.FirstOrDefault(x => x.Id == id);
            _context.Tasks.Remove(oldTask);
            _context.Tasks.Add(newTask);
            newTask.Id = oldTask.Id;
            _context.SaveChanges();
            return newTask.Id;
        }

    }
}
