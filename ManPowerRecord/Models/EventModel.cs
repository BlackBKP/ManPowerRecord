using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class EventModel
    {
        string title { get; set; }
        DateTime start_date { get; set; }
        DateTime stop_date { get; set; }
        bool all_day { get; set; }
        bool editable { get; set; }
    }
}
