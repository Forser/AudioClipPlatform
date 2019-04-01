using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Project_Untitled.Models;
using Project_Untitled.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace Project_Untitled.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<IdentityUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IAccountRepository accountRepository;
        private readonly AppIdentityDbContext context;

        public AccountController(UserManager<IdentityUser> userMgr, RoleManager<IdentityRole> roleMgr, SignInManager<IdentityUser> signInMgr, IEmailSender emailSndr, IAccountRepository accRepository, AppIdentityDbContext ctx)
        {
            userManager = userMgr;
            roleManager = roleMgr;
            signInManager = signInMgr;
            emailSender = emailSndr;
            accountRepository = accRepository;
            context = ctx;

            IdentitySeedData.EnsurePopulated(userMgr, roleManager, context).Wait();
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginViewModel.Name);

                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginViewModel?.ReturnUrl ?? "/");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginViewModel);
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            var ResetPasswordModel = new ResetPasswordViewModel();

            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset");
            }
            else
            {
                ResetPasswordModel = new ResetPasswordViewModel { Code = code };
            }

            return View(ResetPasswordModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel passwordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByEmailAsync(passwordViewModel.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }

            var result = await userManager.ResetPasswordAsync(user, passwordViewModel.Code, passwordViewModel.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("ResetPasswordConfirmation");
        }

        [AllowAnonymous]
        public ViewResult ResetPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult ForgotPassword()
        {

            return View(new ForgotPasswordViewModel());
        }

        public async Task<IActionResult> ChangePassword()
        {
            var user = await userManager.GetUserAsync(User);

            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{ userManager.GetUserId(User) }'.");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([FromForm]PasswordViewModel Input)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.GetUserAsync(User);

            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{ userManager.GetUserId(User) }'.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if(!changePasswordResult.Succeeded)
            {
                foreach(var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            await signInManager.RefreshSignInAsync(user);
            TempData["message"] = "Your password has been changed.";
            return RedirectToAction("Index", "Settings", "user-account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(passwordViewModel.Email);
                if(user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page("/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                await emailSender.SendEmailAsync(
                    passwordViewModel.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{callbackUrl}'>clicking here!</a>");

                return View("ForgotPasswordConfirmation");
            }

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ViewResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userFound = await userManager.FindByEmailAsync(registerViewModel.Email);
                if(userFound == null)
                {
                    var user = new IdentityUser { UserName = registerViewModel.Name, Email = registerViewModel.Email };
                    var result = await userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                    {
                        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { userId = user.Id, code = code });
                        await emailSender.SendEmailAsync(registerViewModel.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{callbackUrl}'>click here</a>");

                        await userManager.AddToRoleAsync(user, "Member");
                        TempData["message"] = $"{user.UserName} has been registered!";
                        return LocalRedirect("/");
                    }
                    ModelState.AddModelError("Username", "Username already taken");
                }
                ModelState.AddModelError("Email", "An account with this email is already registered!");
            }

            return View(registerViewModel);
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return Redirect("/");
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public async Task<RedirectResult> DeleteAccount(string returnUrl = "/")
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            accountRepository.DeleteUser(user);
            await userManager.DeleteAsync(user);
            await signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}