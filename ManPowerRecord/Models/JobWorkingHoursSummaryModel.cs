﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class JobWorkingHoursSummaryModel
    {
        public string job_id { get; set; }
        public int normal_hours { get; set; }
        public int normal_min { get; set; }
        public int ot1_5_hours { get; set; }
        public int ot1_5_min { get; set; }
        public int ot3_0_hours { get; set; }
        public int ot3_0_min { get; set; }
    }
}