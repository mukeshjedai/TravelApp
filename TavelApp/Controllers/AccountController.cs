using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TavelApp.Controllers;

public class AccountController : Controller
{
    
    [AllowAnonymous]
    [HttpGet("signin-google")]
    public IActionResult SignInGoogle()
    {
        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleCallback", "Account")
        };
        return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
    }

    [AllowAnonymous]
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (result.Succeeded)
        {
            // Access the Google user's information:
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // Use this info to either create or authenticate a user in your system

            // ... 

            return RedirectToAction("Index", "Home"); // Redirect to a protected page
        }
        return RedirectToAction("Error"); // Handle authentication failures
    }
}