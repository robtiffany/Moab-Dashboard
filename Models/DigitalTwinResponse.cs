using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    public class DigitalTwinResponse
    {
        public Guid Id { get; set; }
        public Guid SecurityToken { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
