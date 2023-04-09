namespace TavelApp.Models
{
    public interface IQuestionData
    {
        string QuestionText { get; set; }
        string Answer1 { get; set; }
        string Answer2 { get; set; }
        string Answer3 { get; set; }
        string Answer4 { get; set; }
    }
}
