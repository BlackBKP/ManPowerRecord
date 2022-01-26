using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using ManPowerRecord.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Controllers
{
    public class TasksController : Controller
    {
        IJob JobService;
        ITask TaskService;

        public TasksController()
        {
            this.JobService = new JobService();
            this.TaskService = new TaskService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTasks()
        {
            List<TaskModel> tasks = TaskService.GetTasks();
            return Json(tasks);
        }

        [HttpPost]
        public JsonResult AddTask(string task_string)
        {
            TaskModel task = JsonConvert.DeserializeObject<TaskModel>(task_string);
            var result = TaskService.CreateTask(task);
            return Json(result);
        }
    }
}
