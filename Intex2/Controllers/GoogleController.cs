using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Intex2.Controllers
{
    [Authorize]
    public class GoogleController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;


        public class LoginModel : PageModel
        {
            private SignInManager<IdentityUser> signInManager;

            public LoginModel(SignInManager<IdentityUser> signinMgr)
            {
                signInManager = signinMgr;
            }

            [BindProperty]
            [Required]
            public string UserName { get; set; }

            [BindProperty]
            [Required]
            public string Password { get; set; }

            [BindProperty(SupportsGet = true)]
            public string ReturnUrl { get; set; }

            public async Task<IActionResult> OnPostAsync()
            {

                if (ModelState.IsValid)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(UserName, Password,
                            false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(ReturnUrl ?? "/");
                    }
                    ModelState.AddModelError("", "Invalid username or password");
                }

                return Page();
            }
        }

    public GoogleController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        // GET: /<controller>/
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {

            string redirectUrl = Url.Action("GoogleResponse", "Google");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return View("Failed");

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
            {
                IdentityUser user = new IdentityUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
        }


    }
}
