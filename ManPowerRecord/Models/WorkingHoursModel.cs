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
        public string job_1 { get; set; }
        public string task_1 { get; set; }
        public TimeSpan start_time1 { get; set; }
        public TimeSpan stop_time1 { get; set; }
        public string job_2 { get; set; }
        public string task_2 { get; set; }
        public TimeSpan? start_time2 { get; set; }
        public TimeSpan? stop_time2 { get; set; }
        public string job_3 { get; set; }
        public string task_3 { get; set; }
        public TimeSpan? start_time3 { get; set; }
        public TimeSpan? stop_time3 { get; set; }
        public bool lunch { get; set; }
        public bool dinner { get; set; }
        public string note { get; set; }
        public TimeSpan normal { get; set; }
        public TimeSpan ot1_5 { get; set; }
        public TimeSpan ot3_0 { get; set; }
    }
}
