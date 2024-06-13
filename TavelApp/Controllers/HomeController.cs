using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Added for HttpContext

namespace TavelApp.Controllers
{
    public class HomeController : Controller
    {
        // ... (Existing code: Constructor, IWebHostEnvironment)

        public IActionResult Index()
        {
            // 1. Device Detection (Replace with your preferred method)
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            bool isMobileDevice = userAgent.Contains("Mobile") || userAgent.Contains("iPhone") || userAgent.Contains("Android");

            // 2. Redirect Logic (Example: Different views for mobile/desktop)
            if (isMobileDevice)
            {
                return RedirectToAction("Index", "Mobileapp");

            }
            else
            {
                return View(); // Default view for non-mobile devices
            }
        }
    }
}