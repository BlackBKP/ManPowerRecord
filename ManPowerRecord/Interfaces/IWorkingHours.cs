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
        List<WorkingHoursModel> GetWorkingHours(string year, string month);
        string AddWorkingHours(WorkingHoursModel wh);
        string UpdateWorkingHours(WorkingHoursModel wh);
    }
}
