using Charities.Data.Models;
using CharityProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Charities.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/Account/Login");
            }
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                Email = model.Email,
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.Email == "admin@abv.bg")
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/Account/Login");
            }
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            };
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Redirect("/Home/Index");
                }
            }
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> CreateRoles()
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result || !roleManager.RoleExistsAsync("User").Result)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                await roleManager.CreateAsync(new IdentityRole() { Name = "Administrator" });
            }
            return Redirect("/Home/Index");
        }
    }
}
