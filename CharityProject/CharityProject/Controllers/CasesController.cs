using Microsoft.AspNetCore.Mvc;

namespace Charities.Controllers
{
    public class CasesController : Controller
    {
        [HttpGet]
        public IActionResult All()
        {
            return View();
        }
    }
}
