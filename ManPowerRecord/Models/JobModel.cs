using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class JobModel
    {
        string job_id { get; set; }
        string sale_department { get; set; }
        string sale { get; set; }
        int cost { get; set; }
        bool md { get; set; }
        bool pd { get; set; }
        double factor { get; set; }
        double manpower { get; set; }
        double cost_per_manpower { get; set; }
        double ot_manpower { get; set; }
    }
}
