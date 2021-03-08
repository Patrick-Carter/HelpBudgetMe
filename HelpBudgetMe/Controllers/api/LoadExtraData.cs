using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers.api
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class LoadExtraData : ControllerBase
    {

        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public LoadExtraData(ApplicationDBContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetMorePaycheck")]
        public async Task<List<Paycheck>> GetMorePaycheck()
        {
            string Id = _userManager.GetUserId(User);
            User currentUser = await _db.Users.Where(a => a.Id == Id).FirstOrDefaultAsync();

            var paychecks = _db.Paychecks.Where(a => a.User == currentUser).OrderByDescending(b => b.DateCreated).Take(10).ToList();

            return paychecks;
        }
    }
}
