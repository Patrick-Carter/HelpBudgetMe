using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using HelpBudgetMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    [Authorize]
    public class NeedController : Controller
    {
        private readonly IVMGeneratorService _vmGenerator;
        private readonly IItemGeneratorService _itemGenerator;
        private readonly IUserEditorService _userEditor;
        private readonly IItemFetcherService _itemFetcherService;

        public NeedController(IVMGeneratorService vmGenerator,
            IItemGeneratorService itemGenerator,
            IUserEditorService userEditor,
            IItemFetcherService itemFetcherService)
        {
            _vmGenerator = vmGenerator;
            _itemGenerator = itemGenerator;
            _userEditor = userEditor;
            _itemFetcherService = itemFetcherService;
        }

        [HttpGet]
        // Index = GetAllNeeds()
        public IActionResult Index()
        {
            try
            {
                var model =  _vmGenerator.CreateNeededModelForNeedIndex();
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult AddNeed()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditNeed(int Id)
        {
            try 
            {
                var model = await _vmGenerator.CreateNeededModelForEditNeedAsync(Id);
                return View(model);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteNeed(int Id)
        {
            try 
            {
                var model = await _vmGenerator.CreateNeededModelForDeleteNeedAsync(Id);
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult TransferFromNeeds()
        {
            try
            {
                var model = _vmGenerator.CreateNeededModelForTransfer();
               
                return View(model);
            } 
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNeed(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.CreateNeedAndPushToDbAsync(model);

                    // Now I update user money values to be accurate.
                    await _userEditor.SubtractCurrentMoney(model.Amount);
                    await _userEditor.SubtractBudgetedForNeeds(model.Amount);
                    await _userEditor.AddToAllTimeSpent(model.Amount);

                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNeed(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.EditNeedAndPushToDbAsync(model);
                    
                    // I must get the amount changed after the edit to update user money values
                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    // Now I update user money values to be accurate.
                    await _userEditor.AddCurrentMoney(amountChanged);
                    await _userEditor.SubtractFromAllTimeSpent(amountChanged);
                    await _userEditor.AddBudgetedForNeeds(amountChanged);

                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteNeed(Need model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.DeleteNeedAndPushToDbAsync(model);

                    // Now I update user money values to be accurate.
                    await _userEditor.AddCurrentMoney(model.Amount);
                    await _userEditor.AddBudgetedForNeeds(model.Amount);
                    await _userEditor.SubtractFromAllTimeSpent(model.Amount);

                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return BadRequest();
                }
                
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> TransferFromNeeds(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Now I update user money values to be accurate.
                await _userEditor.SubtractBudgetedForNeeds(model.TransferToSavings + model.TransferToWants);
                await _userEditor.AddBudgetedForWants(model.TransferToWants);
                await _userEditor.AddBudgetedForSavings(model.TransferToSavings);

                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }

        [HttpPost]
        [Route("api/GetMoreNeed")]
        public async Task<JsonResult> GetMoreNeeds()
        {
            try
            {
                // get req body and convert text to int
                string req = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                int skip = 10;

                skip = int.Parse(req);
                List<Need> needs = _itemFetcherService.GetMoreNeeds(skip);

                return Json(needs);
            }
            catch
            {
                return Json("Something went wrong");
            }
        }
    }
}