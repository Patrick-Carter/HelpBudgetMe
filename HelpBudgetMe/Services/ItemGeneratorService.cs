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
            User currentUser = await _itemFetcherService.GetUserAsync();

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

        public async Task DeleteNeedAndPushToDbAsync(Need model)
        {
            Need need = await _itemFetcherService.GetSpecificNeedAsync(model.Id);
            _db.Needs.Remove(need);
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
    }
}
