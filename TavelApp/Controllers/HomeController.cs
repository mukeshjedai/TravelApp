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
      public static  int tempbag = 0;

        public IActionResult Index()
        {
            ViewBag.Message = tempbag;

            tempbag = tempbag + 1;
            ViewData["Title"] = "Australian Citizenship Practice";

            // Get the absolute file path to the CSV file
            string questionFilePath = _webHostEnvironment.ContentRootPath + "\\Data\\AustralianCitizenshipQuestionAnswer.csv";

            // Read the CSV file data
            var reader = new StreamReader(questionFilePath);
            var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

            // Parse the data into a list of QuestionData objects
            var questionDataList = csv.GetRecords<QuestionData>().ToList();

            // Set the Questions property of the IndexModel instance

            // Pass the IndexModel instance to the View method
            return View();
        }

    }

    public class QuestionData
    {
        [Name("Question")]
        public string QuestionText { get; set; }

        [Name("Option1")]
        public string Answer1 { get; set; }

        [Name("Option2")]
        public string Answer2 { get; set; }

        [Name("Option3")]
        public string Answer3 { get; set; }

        [Name("Option4")]
        public string Answer4 { get; set; }
    }
}
