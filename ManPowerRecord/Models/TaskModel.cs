﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class TaskModel
    {
        public string job_id { get; set; }
        public string task_id { get; set; }
        public string task_name { get; set; }
        public string task_description { get; set; }
        public string status { get; set; }
    }
}
