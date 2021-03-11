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
    public class WantController : Controller
    {
        private readonly IVMGeneratorService _vmGenerator;
        private readonly IItemGeneratorService _itemGenerator;
        private readonly IUserEditorService _userEditor;
        private readonly IItemFetcherService _itemFetcherService;

        public WantController(IVMGeneratorService vmGenerator,
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
                WantsViewModel model = _vmGenerator.CreateNeededModelForWantsIndex();
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult AddWant()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditWant(int Id)
        {
            try
            {
                var model = await _vmGenerator.CreateNeededModelForEditWants(Id);
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteWant(int Id)
        {
            var model = await _vmGenerator.CreateNeededModelForDeleteWant(Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult TransferFromWants()
        {
            var model = _vmGenerator.CreateNeededModelForTransfer();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWant(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.CreateWantAndPushToDbAsync(model);

                    await _userEditor.SubtractCurrentMoney(model.Amount);
                    await _userEditor.SubtractBudgetedForWants(model.Amount);
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

        public async Task<IActionResult> EditWant(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.EditWantAndPushToDbAsync(model);

                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    await _userEditor.AddCurrentMoney(amountChanged);
                    await _userEditor.SubtractFromAllTimeSpent(amountChanged);
                    await _userEditor.AddBudgetedForWants(amountChanged);

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

        public async Task<IActionResult> DeleteWant(Want model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemGenerator.DeleteWantAndPushToDbAsync(model);

                    await _userEditor.AddCurrentMoney(model.Amount);
                    await _userEditor.AddBudgetedForWants(model.Amount);
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
        public async Task<IActionResult> TransferFromWants(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userEditor.SubtractBudgetedForWants(model.TransferToSavings + model.TransferToNeeds);
                await _userEditor.AddBudgetedForNeeds(model.TransferToNeeds);
                await _userEditor.AddBudgetedForSavings(model.TransferToSavings);

                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }

        [HttpPost]
        [Route("api/GetMoreWant")]

        public async Task<JsonResult> GetMorePaycheck()
        {
            try
            {
                // get req body and convert text to int
                string req = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                int skip = 10;
                skip = int.Parse(req);


                var wants = _itemFetcherService.GetMoreWants(skip);

                return Json(wants);
            }
            catch
            {
                return Json("Something went wrong");
            }
        }    
    }
}
