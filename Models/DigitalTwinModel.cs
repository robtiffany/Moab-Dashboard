﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoabDashboard.Models
{
    public class DigitalTwinModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Version { get; set; }
        public Guid Organization { get; set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
