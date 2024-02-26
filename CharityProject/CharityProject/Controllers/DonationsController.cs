using Charities.Data.Models;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CharityProject.Controllers
{
    public class DonationsController : Controller
    {
        private readonly ICaseService caseService;
        private readonly IDonationService donationService;
        private readonly UserManager<User> userManager;

        public DonationsController(ICaseService _caseService, IDonationService _donationService, UserManager<User> _userManager)
        {
            this.caseService = _caseService;
            this.donationService = _donationService;
            this.userManager = _userManager;
        }

        [HttpPost]
        public async Task<IActionResult> MakeDonation(DetailsCaseViewModel model)
        {
            Donation donation = new Donation()
            {
                Amount = model.Donation.Amount,
                UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                CharityId = model.Charity.Id,
                IsAnonymous = model.Donation.IsAnonymous,
                DateMade = DateTime.Now,
                Comment = model.Donation.Comment,
            };
            Charity charity = await caseService.GetCharity(model.Charity.Id);
            charity = await donationService.MakeDonationToCharity(donation, charity);
            return RedirectToAction("DetailsCase", "Cases", charity);
        }
    }
}
