using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClinicBooking.Models.ViewModels;
using ClinicBooking.Models;

namespace ClinicBooking.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userMgr, SignInManager<ApplicationUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName 
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Redirect(model.ReturnUrl ?? "/Home/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> UserDashboard()
        {
            var user = await userManager.GetUserAsync(User) as ApplicationUser;
            if (user == null) return NotFound();

            var model = new UserProfileViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }




        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDashboard(UserProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userManager.GetUserAsync(User) as ApplicationUser;
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var updateResult = await userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            if (updateResult.Succeeded && ModelState.IsValid)
            {
                ViewBag.Message = "Профіль оновлено.";
            }

            return View(model);
        }

    }
}
