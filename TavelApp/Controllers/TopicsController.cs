using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;

namespace TravelApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : Controller
    {
        
        private readonly IWebHostEnvironment _webHostEnvironment;
        int tempbag = 0;

        public TopicsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            
            
            ViewBag.Message = tempbag;

            tempbag++;
            return View();
        }
    

   
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           
            // Retrieve data from a data source (e.g. database, file, API)
            var data = new List<CitizenshipQuestionAnswer> { };

            //string questionFilePath = _webHostEnvironment.ContentRootPath + "//Data//AustralianCitizenshipQuestionAnswer.csv";

            string path =  _webHostEnvironment.ContentRootPath + "//Data//AustralianCitizenshipQuestionAnswer.csv";
            // Create a new StreamReader to read the CSV file
            using (var reader = new StreamReader(path))
            {
                // Create a new CsvReader to parse the CSV file
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // Read the CSV data into a list of CitizenshipQuestionAnswer objects
                     data = csv.GetRecords<CitizenshipQuestionAnswer>().ToList();

                    // Return the data as a JSON object
                    return Json(data);
                }
            }
        }
    }

    public class CitizenshipQuestionAnswer
    {
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string answer { get; set; }
    }


}
