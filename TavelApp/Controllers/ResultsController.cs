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
    


}