﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Controllers
{
    public class WorkingHoursController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
