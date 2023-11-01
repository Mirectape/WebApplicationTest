using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.AuthPersonApp;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly ILogger _log;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILoggerFactory log)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_log = log.CreateLogger("My logger");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            //_log.LogWarning($" ------- \n >> Login(string returnUrl) worked successfully, returnUrl = {returnUrl}\n ------- \n ");

            return View(new UserLogin()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
                var loginResult = await _signInManager.PasswordSignInAsync(model.Username,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("AllView", "Home");
                }

            ModelState.AddModelError("", "User is not found");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("AllView", "Home");
                }
                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("AllView", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Edit", "Account");
        }
    }
}
