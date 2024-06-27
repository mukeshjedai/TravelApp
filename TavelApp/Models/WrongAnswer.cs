namespace TavelApp.Models;

using System.Text.Json.Serialization;

public class WrongAnswer
{
    
    private static int _nextId = 1;

    [JsonPropertyName("AutoId")]
    public int AutoId { get; private set; }

    public WrongAnswer()
    {
        AutoId = System.Threading.Interlocked.Increment(ref _nextId);
    }
    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("email")]
    public string email { get; set; }

    [JsonPropertyName("Question")]
    public string Question { get; set; }

    [JsonPropertyName("Option1")]
    public string Option1 { get; set; }

    [JsonPropertyName("Option2")]
    public string Option2 { get; set; }

    [JsonPropertyName("Option3")]
    public string Option3 { get; set; }

    [JsonPropertyName("Option4")]
    public string Option4 { get; set; }

    [JsonPropertyName("answer")]
    public int answer { get; set; }

    [JsonPropertyName("AnswerDatetime")]
    public DateTime AnswerDatetime { get; set; }
    
    [JsonPropertyName("question_number")]
    public string question_number { get; set; }
}