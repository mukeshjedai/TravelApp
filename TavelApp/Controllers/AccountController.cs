using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace TravelApp.Controllers // Replace with your actual namespace
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return new ChallengeResult(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleCallback")
                });
        }


        [HttpGet]
        public async Task<IActionResult> GoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded || !authenticateResult.Principal.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Login");
            }

            // (Optional) Access Google user information here:
            // var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            // var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            // ... more claims

            return LocalRedirect((authenticateResult.Properties.Items["returnUrl"]) ?? "/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}