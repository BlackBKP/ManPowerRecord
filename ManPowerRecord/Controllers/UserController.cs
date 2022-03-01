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
    public class UserController : Controller
    {
        IUser UserService;

        public UserController()
        {
            this.UserService = new UserService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            List<UserModel> users = UserService.GetUsers().OrderBy(o => o.user_id).ToList();
            return Json(users);
        }

        [HttpPost]
        public JsonResult CreateUser(string user_string)
        {
            UserModel user = JsonConvert.DeserializeObject<UserModel>(user_string);
            var result = UserService.CreateUser(user);
            return Json(result);
        }

        [HttpPatch]
        public JsonResult UpdateUser(string user_string)
        {
            UserModel user = JsonConvert.DeserializeObject<UserModel>(user_string);
            var result = UserService.UpdateUser(user);
            return Json(result);
        }
    }
}
