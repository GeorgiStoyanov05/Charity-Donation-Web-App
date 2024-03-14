using Charities.Data.Models;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CharityProject.Controllers
{
    public class UpdatesController : Controller
    {
        private readonly ICaseService caseService;
        private readonly IUpdateService updateService;

        public UpdatesController(ICaseService _caseService, IUpdateService _updateService)
        {
            this.caseService = _caseService;
            this.updateService = _updateService;
        }
        public async Task<IActionResult> PostUpdate(DetailsCaseViewModel model)
        {
            Update update = new Update()
            {
                Text = model.Update.Text,
                CharityId = model.Charity.Id,
                DateCreated = DateTime.Now,
                UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
            };
            Charity charity = await caseService.GetCharity(model.Charity.Id);
            await updateService.PostUpdateToCharity(update, charity);
            return RedirectToAction("DetailsCase", "Cases", new { id = model.Charity.Id });
        }

        public async Task<IActionResult> DeleteUpdate(Guid updateId, Guid charityId)
        {
            Charity charity = await caseService.GetCharity(charityId);
            Update update = updateService.GetUpdate(updateId, charity);
            await updateService.DeleteUpdateFromCharity(update, charity);
            return RedirectToAction("DetailsCase", "Cases", new { id = charityId });
        }
    }
}
