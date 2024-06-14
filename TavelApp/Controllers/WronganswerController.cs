using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TavelApp.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace TavelApp.Controllers;


[Route("api/[controller]")]
public class WronganswerController : Controller
{
    
    private readonly IConfiguration _configuration; // For reading connection string

    public WronganswerController(IConfiguration configuration)
    {
        _configuration = configuration;
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

        // 3. SQL Command Preparation
        var command = new NpgsqlCommand("INSERT INTO user_wronganswers (email, question_number) VALUES (@email, @question_number)", connection);
        command.Parameters.AddWithValue("@email", wrongAnswer.email);
        command.Parameters.AddWithValue("@question_number", Int32.Parse(wrongAnswer.question_number));

        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            return Ok("User Exists");
        }
        
        return BadRequest("Wrong Answer Recorded."); 
    }
}