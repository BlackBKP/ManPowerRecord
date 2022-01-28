using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class WorkingHoursModel
    {
        public string user_id { get; set; }
        public string working_date { get; set; }
        public string job_id { get; set; }
        public string task_id { get; set; }
        public string start_time { get; set; }
        public string stop_time { get; set; }
        public bool lunch { get; set; }
        public string lunch_start { get; set; }
        public string lunch_stop { get; set; }
        public bool break_evening { get; set; }
        public string break_start { get; set; }
        public string break_stop { get; set; }
        public string note { get; set; }
    }
}
