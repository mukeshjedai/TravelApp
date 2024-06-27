using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TavelApp.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Dapper;

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
        var connectionString = _configuration.GetConnectionString("AzurePostgresConnection");

        using var connection = new NpgsqlConnection(connectionString);
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
        
        var connectionString = _configuration.GetConnectionString("AzurePostgresConnection");

        // 2. Npgsql Connection Object
        using var connection = new NpgsqlConnection(connectionString);
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
        
        var connectionString = _configuration.GetConnectionString("AzurePostgresConnection");

        // 2. Npgsql Connection Object
        using var connection = new NpgsqlConnection(connectionString);
        //connection.Open();
        await connection.OpenAsync();

        var query =
            "SELECT Que.id, email, question_number,Que.\"Question\",\"Option1\", \"Option2\", \"Option3\", \"Option4\", answer, answer_datetime FROM public.user_wronganswers as wa " +
            "inner join public.\"Questions\" as Que on wa.question_number = Que.id where wa.email = @email";

        var wrongAnswers = await connection.QueryAsync<WrongAnswer>(
            query,
            new { Email = email });
        connection.Close();
        return Ok(wrongAnswers);

        
    }



    // GET
    [HttpPost("postwronganswer")] // Use a descriptive endpoint
    public IActionResult Index([FromBody] WrongAnswer wrongAnswer)
    {
        
        var connectionString = _configuration.GetConnectionString("AzurePostgresConnection");

        // 2. Npgsql Connection Object
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        if (wrongAnswer.email == null)
        {
            return BadRequest("please signin to save score");
        }

      if ( CheckIfAnswerExistsAsync(wrongAnswer.email,(int.Parse(wrongAnswer.question_number))))
      { return Ok("Already Saved.");} 


        // 3. SQL Command Preparation
        var command = new NpgsqlCommand("INSERT INTO user_wronganswers (email, question_number) VALUES (@email, @question_number)", connection);
        command.Parameters.AddWithValue("@email", wrongAnswer.email);
        command.Parameters.AddWithValue("@question_number", Int32.Parse(wrongAnswer.question_number));

        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            return Ok("Wrong answer recorded");
        }
        
        return BadRequest("Error Recording wrong answer"); 
    }
}