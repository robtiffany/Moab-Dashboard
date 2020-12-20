﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    public class DigitalTwinLimitedResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long SecurityToken { get; set; }
        public DateTime Created { get; set; }
        public long DigitalTwinModel { get; set; }
        public long Group { get; set; }
        public long Enabled { get; set; }
    }
}
