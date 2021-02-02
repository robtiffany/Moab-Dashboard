using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    class DigitalThreadEventRequest
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid DigitalTwin { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
