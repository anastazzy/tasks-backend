using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Dto
{
    public class TaskDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UserDto User { get; set; }
    }
}
