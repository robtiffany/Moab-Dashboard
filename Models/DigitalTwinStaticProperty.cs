using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    class DigitalTwinStaticProperty
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public long DigitalTwin { get; set; }
    }
}
