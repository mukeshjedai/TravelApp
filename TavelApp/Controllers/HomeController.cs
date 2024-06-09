using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using TavelApp.Pages;
using TavelApp.Views;

namespace TavelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController( IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Australian Citizenship Practice";

            return View();
        }

    }
    
}
