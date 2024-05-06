using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Abstract;

namespace Website.Domain.Entities
{
    public class User : Entity
    {
        public string Username{ get; set; }
        public string Password{ get; set; }
        public string Email { get; set; }
        public int Role{ get; set; }
    }
}
