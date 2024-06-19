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
            "SELECT * FROM user_wronganswers WHERE email = @Email",
            new { Email = email });
        connection.Close();
        return Ok(wrongAnswers);

        
    }

}