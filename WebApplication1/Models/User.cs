using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<Task> Tasks { get; set; }

    }

}
