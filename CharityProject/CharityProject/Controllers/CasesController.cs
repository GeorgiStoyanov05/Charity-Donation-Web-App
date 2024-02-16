using Charities.Data.Models;
using CharityProject.Contracts;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Charities.Controllers
{
    public class CasesController : Controller
    {
        private readonly ICaseService caseService;

        public CasesController(ICaseService caseService)
        {
            this.caseService = caseService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<Charity> charities = await caseService.GetAllCharities();
            return View(charities);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await caseService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCaseViewModel model)
        {
            var userId = User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)?.Value;
            var charity = await caseService.CreateCharity(model, userId);
            return Redirect("/Home/Index");
        }
    }
}
