using System.Drawing;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TavelApp.Models;

namespace TavelApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ResultsController : Controller
{

    public IConfiguration _configuration;

    public ResultsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("getwronganswers")] // Use a descriptive endpoint
    public async Task<ActionResult<IEnumerable<WrongAnswer>>> GetUsersByEmail(string email)
    {
        
        var connectionString = _configuration.GetConnectionString("AzurePostgresConnection");

        // 2. Npgsql Connection Object
        using var connection = new NpgsqlConnection(connectionString);
        //connection.Open();
        await connection.OpenAsync();
        
        

        var wrongAnswers = await connection.QueryAsync<WrongAnswer>(
            "SELECT  Question, Option1, Option2, Option3, Option4, answer FROM public.Questions as que inner join user_wronganswers as wa on  wa.question_number = que.id where wa.email = @email",
            new { Email = email });
        connection.Close();
        return Ok(wrongAnswers);

        
    }

}