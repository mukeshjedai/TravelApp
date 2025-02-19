using Microsoft.AspNetCore.Mvc;
using TavelApp.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Dapper;
using Microsoft.Data.SqlClient;

namespace TavelApp.Controllers;


[Route("api/[controller]")]
public class WronganswerController : Controller
{
    
    private readonly IConfiguration _configuration; // For reading connection string

    public WronganswerController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool CheckIfAnswerExistsAsync(string email, int question_number) // Add the question_number parameter
    {
        var connectionString = _configuration.GetConnectionString("LocalDBConnection");

        using var connection = new SqlConnection(connectionString);
         connection.Open();

        int wrongAnswers =  connection.QuerySingle<int>(
            "SELECT COUNT(*) FROM user_wronganswers WHERE email = @Email AND question_number = @QuestionNumber", // Correct parameter name
            new { Email = email, QuestionNumber = question_number });  // Pass both parameters
        connection.Close();
        return wrongAnswers > 0;
    }
    
    [HttpGet("getWronganswers")] // Use a descriptive endpoint
    public async Task<ActionResult<IEnumerable<WrongAnswer>>> Get(string email)
    {
        
        var connectionString = _configuration.GetConnectionString("LocalDBConnection");

        // 2. Npgsql Connection Object
        using var connection = new SqlConnection(connectionString);
        //connection.Open();
        await connection.OpenAsync();
        
        

        var wrongAnswers = await connection.QueryAsync<QuestionData>(
            "SELECT  \"Question\", \"Option1\", \"Option2\", \"Option3\", \"Option4\", answer FROM public.\"Questions\" as que inner join user_wronganswers as wa on  wa.question_number = que.id where wa.email = @email",
            new { Email = email });
        connection.Close();
        return Ok(wrongAnswers);

        
    }
    
    [HttpGet("GetWronganswersSummary")] // Use a descriptive endpoint
    public async Task<ActionResult<IEnumerable<WrongAnswer>>> GetWronganswersSummary(string email)
    {
        
        var connectionString = _configuration.GetConnectionString("LocalDBConnection");

        // 2. Npgsql Connection Object
        using var connection = new SqlConnection(connectionString);
        //connection.Open();
        await connection.OpenAsync();

        var query =
            "SELECT Que.id, email, question_number,Que.\"Question\" as Question,\"Option1\" as Option1, \"Option2\" as Option2, \"Option3\" as Option3, \"Option4\" as Option4, answer as answer, answer_datetime as answer_datetime FROM public.user_wronganswers as wa " +
            "inner join public.\"Questions\" as Que on wa.question_number = Que.id where wa.email = @email";

        var wrongAnswers = await connection.QueryAsync<WrongAnswer>(
            query,
            new { Email = email });
        connection.Close();
        return Ok(wrongAnswers);

        
    }


    [HttpPost("postwronganswer")] // Use a descriptive endpoint
    public IActionResult Index([FromBody] WrongAnswer wrongAnswer)
    {
        WrongAnswer wrongAnswer1 = new WrongAnswer
        {
            id = 1,
            email = "user@example.com",
            Question = "What is the capital of France?",
            Option1 = "Berlin",
            Option2 = "Madrid",
            Option3 = "Paris",
            Option4 = "Rome",
            answer = 3, // Assuming the correct answer is option 3 (Paris)
            AnswerDatetime = DateTime.Now,
            question_number = "1"
        };

        wrongAnswer = wrongAnswer1;
        // Use LocalDB connection string
        var connectionString = _configuration.GetConnectionString("LocalDBConnection");

        // SQL Server Connection Object
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        if (wrongAnswer.email == null)
        {
            return BadRequest("Please sign in to save score");
        }

        if (CheckIfAnswerExistsAsync(wrongAnswer.email, int.Parse(wrongAnswer.question_number)))
        {
            return Ok("Already Saved.");
        }

        // SQL Command Preparation
        var command = new SqlCommand("INSERT INTO user_wronganswers (email, question_number) VALUES (@email, @question_number)", connection);
        command.Parameters.AddWithValue("@email", wrongAnswer.email);
        command.Parameters.AddWithValue("@question_number", int.Parse(wrongAnswer.question_number));

        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            return Ok("Wrong answer recorded");
        }

        return BadRequest("Error recording wrong answer");
    }
}