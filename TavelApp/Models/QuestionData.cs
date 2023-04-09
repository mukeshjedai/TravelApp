using TavelApp.Controllers;

namespace TavelApp.Models
{
    public class QuestionData : IQuestionData
    {
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
    }
}
