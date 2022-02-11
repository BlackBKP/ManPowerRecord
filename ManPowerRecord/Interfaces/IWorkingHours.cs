using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Interfaces
{
    interface IWorkingHours
    {
        List<WorkingHoursModel> GetWorkingHours();
        string AddWorkingHours(WorkingHoursModel wh);
        string UpdateWorkingHours(WorkingHoursModel wh);
    }
}
