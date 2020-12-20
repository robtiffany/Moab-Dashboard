using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    public class UserResponse
    {
        public long Id { get; set; }
        public long SecurityToken { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
