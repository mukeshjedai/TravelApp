using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace TravelApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : Controller
    {
        
        private readonly ILogger<TopicsController> _logger; 

        public TopicsController(ILogger<TopicsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //ViewBag.Message = tempbag++;
            return View();
        }

        [HttpGet]
        
        public ActionResult<IEnumerable<CitizenshipQuestionAnswer>> Get()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string csvUrl = "https://raw.githubusercontent.com/mukeshjedai/Auscitfiles/main/AustralianCitizenshipQuestionAnswer.csv";
                    using (MemoryStream stream = new MemoryStream(client.DownloadData(csvUrl)))
                    using (StreamReader reader = new StreamReader(stream))
                    using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var data = csv.GetRecords<CitizenshipQuestionAnswer>().ToList();
                        return Json(data);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading or parsing CSV"); 
                return StatusCode(500, "Internal server error"); 
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
