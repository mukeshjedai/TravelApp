using Microsoft.AspNetCore.Mvc;

namespace TavelApp.Controllers;

public class GoogleLogin : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}