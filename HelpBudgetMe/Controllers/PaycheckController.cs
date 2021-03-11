using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using HelpBudgetMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace HelpBudgetMe.Controllers
{
    [Authorize]
    
    public class PaycheckController : Controller
    {
        private readonly IVMGeneratorService _vmGenerator;
        private readonly IItemGeneratorService _itemGenerator;
        private readonly IUserEditorService _userEditor;
        private readonly IItemFetcherService _itemFetcherService;

        public PaycheckController(IVMGeneratorService vmGenerator,
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
        // Index = GetAllPaychecks
        public IActionResult Index()
        {
            try
            {
                PaychecksViewModel model = _vmGenerator.CreateModelForPaycheckIndex();
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]

        public IActionResult AddPaycheck()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditPaycheck(int Id)
        {
            EditViewModel model = await _vmGenerator.CreateNeededModelForEditPaycheckAsync(Id);

            return View(model);
        }

        public async Task<IActionResult> DeletePaycheck(int Id)
        {

            Paycheck paycheck = await _vmGenerator.CreateNeededModelForDeletePaycheckAsync(Id);

            return View(paycheck);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPaycheck(AddViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.CreatePaycheckAndPushToDbAsync(model);

                    await _userEditor.AddCurrentMoney(model.Amount);
                    await _userEditor.AddToAllTimeEarned(model.Amount);
                    await _userEditor.AddBudgetedForNeeds(model.Amount * .5m);
                    await _userEditor.AddBudgetedForWants(model.Amount * .3m);
                    await _userEditor.AddBudgetedForSavings(model.Amount * .2m);

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
        public async Task<IActionResult> EditPaycheck(EditViewModel model)
        {
            if (ModelState.IsValid) { 
                try {
                    await _itemGenerator.EditPaycheckAndPushToDbAsync(model);

                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    await _userEditor.SubtractCurrentMoney(amountChanged);
                    await _userEditor.SubtractBudgetedForNeeds(amountChanged * .5m);
                    await _userEditor.SubtractBudgetedForWants(amountChanged * .3m);
                    await _userEditor.SubtractBudgetedForSavings(amountChanged * .2m);
                    await _userEditor.SubtractFromAllTimeEarned(amountChanged);

                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeletePaycheck(Paycheck model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.DeletePaycheckAndPushToDbAstnc(model);

                    await _userEditor.SubtractCurrentMoney(model.Amount);
                    await _userEditor.SubtractBudgetedForNeeds(model.Amount * .5m);
                    await _userEditor.SubtractBudgetedForWants(model.Amount * .3m);
                    await _userEditor.SubtractBudgetedForSavings(model.Amount * .2m);
                    await _userEditor.SubtractFromAllTimeEarned(model.Amount);

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
        [Route("api/GetMorePaycheck")]
        public async Task<JsonResult> GetMorePaycheck()
        {
            try
            {
                // get req body and convert text to int
                string req = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                int skip = 10;

                skip = int.Parse(req);

                List<Paycheck> paychecks = _itemFetcherService.GetMorePaychecks(skip);

                return Json(paychecks);
            }
            catch
            {
                return Json("Something went wrong");
            }
        }

    }
}
