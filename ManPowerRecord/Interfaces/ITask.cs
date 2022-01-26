using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Interfaces
{
    interface ITask
    {
        List<TaskModel> GetTasks();

        string CreateTask(TaskModel task);
    }
}
