using Charities.Data.Models;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Charities.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICaseService caseService;

        public HomeController(ILogger<HomeController> logger, ICaseService _caseService)
        {
            _logger = logger;
            caseService = _caseService;
        }

        public async Task<IActionResult> Index()
        {
            List<Charity> charities = await caseService.GetAllCharities();
            List<int> data = await caseService.ExtractCountData();
            ViewBag.DonationsCount = data[0];
            ViewBag.TotalFundsDonated = data[1];
            ViewBag.VolunteersCount = data[2];
            ViewBag.ProjectsCount = data[3];
            return View(charities);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}