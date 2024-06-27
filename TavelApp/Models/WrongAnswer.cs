namespace TavelApp.Models;

public class WrongAnswer
{
    
    public String id { get; set; }
    public string email { get; set; }
    public string question_number { get; set; }

    public DateTime answer_datetime { get; set; }
    public string Question { get; set; }
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string Option4 { get; set; }
    public string answer { get; set; }
}