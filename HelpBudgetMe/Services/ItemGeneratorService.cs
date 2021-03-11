using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public class ItemGeneratorService : IItemGeneratorService
    {
        private readonly ApplicationDBContext _db;
        private readonly IItemFetcherService _itemFetcherService;
        public ItemGeneratorService(ApplicationDBContext db,
            IItemFetcherService itemFetcherService)
        {
            _db = db;
            _itemFetcherService = itemFetcherService;
        }
        public async Task CreateNeedAndPushToDbAsync(AddViewModel model)
        {
            User currentUser = _itemFetcherService.GetUser();

            Need need = new Need()
            {
                Name = model.Name,
                Amount = model.Amount,
                User = currentUser,
                DateCreated = DateTime.Now
            };

            await _db.Needs.AddAsync(need);
            await _db.SaveChangesAsync();
        }

        public async Task CreatePaycheckAndPushToDbAsync(AddViewModel model)
        {
            User currentUser = _itemFetcherService.GetUser();

            Paycheck paycheck = new Paycheck
            {
                Name = model.Name,
                Amount = model.Amount,
                User = currentUser,
                DateCreated = DateTime.Now
            };

            await _db.Paychecks.AddAsync(paycheck);
            await _db.SaveChangesAsync();
        }

        public async Task CreateSavingAndPushToDbAsync(AddViewModel model)
        {
            User currentUser = _itemFetcherService.GetUser();

            Saving saving = new Saving()
            {
                Name = model.Name,
                Amount = model.Amount,
                User = currentUser,
                DateCreated = DateTime.Now
            };

            await _db.Savings.AddAsync(saving);
            await _db.SaveChangesAsync();
        }

        public async Task CreateWantAndPushToDbAsync(AddViewModel model)
        {
            User currentUser = _itemFetcherService.GetUser();

            Want want = new Want()
            {
                Name = model.Name,
                Amount = model.Amount,
                User = currentUser,
                DateCreated = DateTime.Now
            };

            await _db.Wants.AddAsync(want);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteNeedAndPushToDbAsync(Need model)
        {
            Need need = await _itemFetcherService.GetSpecificNeedAsync(model.Id);
            _db.Needs.Remove(need);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePaycheckAndPushToDbAsync(Paycheck model)
        {
            Paycheck paycheck =  await _itemFetcherService.GetSpecificPaycheckAsync(model.Id);
            _db.Paychecks.Remove(paycheck);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteSavingAndPushToDbAsync(Saving model)
        {
            Saving saving = await _itemFetcherService.GetSpecificSavingAsync(model.Id);
            _db.Savings.Remove(saving);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWantAndPushToDbAsync(Want model)
        {
            Want want = await _itemFetcherService.GetSpecificWantAsync(model.Id);
            _db.Wants.Remove(want);
            await _db.SaveChangesAsync();
        }

        public async Task EditNeedAndPushToDbAsync(EditViewModel model)
        {
            Need need = await _itemFetcherService.GetSpecificNeedAsync(model.Id);

            need.Amount = model.Amount;
            need.Name = model.Name;

            _db.Needs.Update(need);
            await _db.SaveChangesAsync();
        }

        public async Task EditPaycheckAndPushToDbAsync(EditViewModel model)
        {
            Paycheck paycheck = await _itemFetcherService.GetSpecificPaycheckAsync(model.Id);

            paycheck.Amount = model.Amount;
            paycheck.Name = model.Name;

            _db.Paychecks.Update(paycheck);
            await _db.SaveChangesAsync();
        }

        public async Task EditSavingAndPushToDbAsync(EditViewModel model)
        {
            Saving saving = await _itemFetcherService.GetSpecificSavingAsync(model.Id);

            saving.Amount = model.Amount;
            saving.Name = model.Name;

            _db.Savings.Update(saving);
            await _db.SaveChangesAsync();
        }

        public async Task EditWantAndPushToDbAsync(EditViewModel model)
        {
            Want want = await _itemFetcherService.GetSpecificWantAsync(model.Id);

            want.Name = model.Name;
            want.Amount = model.Amount;

            _db.Wants.Update(want);
            await _db.SaveChangesAsync();
        }
    }
}
