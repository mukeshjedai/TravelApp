namespace TavelApp.Models;

public class WrongAnswer
{
    
    public String id { get; set; }
    public string email { get; set; }
    
    
    public string Question { get; set; }
    public string question_number { get; set; }

    public DateTime answer_datetime { get; set; }
}