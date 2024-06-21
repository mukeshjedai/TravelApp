using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TavelApp.Models;

namespace TavelApp.Views.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IQuestionData _questionData;

        public IndexModel(ILogger<IndexModel> logger, IQuestionData questionData)
        {
            _logger = logger;
            _questionData = questionData;
        }


        public void OnGet()
        {
        }
    }
}
