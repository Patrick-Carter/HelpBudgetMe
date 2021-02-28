using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    public class DashBoardController : Controller
    {
       [Authorize]
        public IActionResult Index()
        {
            /*if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Signin", "Account");
            }*/
            return  View();
        }
    }
}
