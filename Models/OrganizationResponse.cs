using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    public class OrganizationResponse
    {
        public Guid id { get; set; }
        public Guid securityToken { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
