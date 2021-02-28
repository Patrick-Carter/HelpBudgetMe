using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    public class WantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
