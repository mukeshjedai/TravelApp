using Microsoft.AspNetCore.Mvc;

namespace TavelApp.Controllers;

public class PrivacyController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}