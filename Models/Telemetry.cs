﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    public class Telemetry
    {
        public long Id { get; set; }
        public long DigitalTwin { get; set; }
        public string DigitalTwinData { get; set; }
        public long DigitalTwinModel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
