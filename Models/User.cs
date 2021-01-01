using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string EmailAddress { get; set; }
        public long Role { get; set; }
        public Guid Organization { get; set; }
        public string Password { get; set; }
        public Guid SecurityToken { get; set; }
        public long PrimaryUser { get; set; }
    }
}
