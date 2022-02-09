using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class WorkingHoursModel
    {
        public int index { get; set; }
        public string user_id { get; set; }
        public DateTime working_date { get; set; }
        public string job_id { get; set; }
        public string task_id { get; set; }
        public string start_time { get; set; }
        public string stop_time { get; set; }
        public bool lunch { get; set; }
        public bool dinner { get; set; }
        public string note { get; set; }
    }
}
