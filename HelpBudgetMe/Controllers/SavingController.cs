using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using HelpBudgetMe.Services;
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
    public class SavingController : Controller
    {

        private readonly IVMGeneratorService _vmGenerator;
        private readonly IItemGeneratorService _itemGenerator;
        private readonly IUserEditorService _userEditor;
        private readonly IItemFetcherService _itemFetcherService;

        public SavingController(IVMGeneratorService vmGenerator,
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
        public IActionResult Index()
        {
            try
            {
                var model = _vmGenerator.CreateNeededModelForSavingIndex();
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult AddSaving()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditSaving(int Id)
        {
            try
            {
                var model = await _vmGenerator.CreateNeededModelForEditSavingAsync(Id);
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSaving(int Id)
        {
            try
            {
                var model = await _vmGenerator.CreateNeededModelForDeleteSavingAsync(Id);
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult TransferFromSavings()
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
        public async Task<IActionResult> AddSaving(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.CreateSavingAndPushToDbAsync(model);

                    await _userEditor.SubtractCurrentMoney(model.Amount);
                    await _userEditor.SubtractBudgetedForSavings(model.Amount);
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

        public async Task<IActionResult> EditSaving(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _itemGenerator.EditSavingAndPushToDbAsync(model);

                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    await _userEditor.AddCurrentMoney(amountChanged);
                    await _userEditor.SubtractFromAllTimeSpent(amountChanged);
                    await _userEditor.AddBudgetedForSavings(amountChanged);
                   
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

        public async Task<IActionResult> DeleteSaving(Saving model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.DeleteSavingAndPushToDbAsync(model);

                    await _userEditor.AddCurrentMoney(model.Amount);
                    await _userEditor.AddBudgetedForSavings(model.Amount);
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
        public async Task<IActionResult> TransferFromSavings(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userEditor.SubtractBudgetedForSavings(model.TransferToWants + model.TransferToNeeds);
                await _userEditor.AddBudgetedForNeeds(model.TransferToNeeds);
                await _userEditor.AddBudgetedForWants(model.TransferToWants);
                
                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }

        [HttpPost]
        [Route("api/GetMoreSaving")]

        public async Task<JsonResult> GetMorePaycheck()
        {

            
            try
            {
                // get req body and convert text to int
                string req = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                int skip = 10;

                skip = int.Parse(req);
                List<Saving> savings = _itemFetcherService.GetMoreSavings(skip);

                return Json(savings);
            }
            catch
            {
                return Json("Something went wrong");
            }

            
        }
    }
}

