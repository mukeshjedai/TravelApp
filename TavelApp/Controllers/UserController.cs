using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using TavelApp.Models;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration; // For reading connection string
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        [HttpPost("register")] // Use a descriptive endpoint
        public IActionResult RegisterUser([FromBody] UserRegistration userData)
        {
            
          
            
            var connectionString = _configuration.GetConnectionString("AzurePostgresConnection");

            // 2. Npgsql Connection Object
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            // 3. SQL Command Preparation
            var userexists = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE email = @email", connection);
            userexists.Parameters.AddWithValue("@email", userData.email);
            int existingUserCount = (int)(long)userexists.ExecuteScalar();

            if (existingUserCount > 0)
            {
                return Ok("User Exists");
            }
            
            

            // 3. SQL Command Preparation
            var command = new NpgsqlCommand("INSERT INTO users (name, email, picture) VALUES (@username, @email, @passwordHash)", connection);
            command.Parameters.AddWithValue("@username", userData.name);
            command.Parameters.AddWithValue("@email", userData.email);
            command.Parameters.AddWithValue("@passwordHash", userData.picture); // Assume you're hashing passwords

            // 4. Data Insertion and Error Handling
            try
            {
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("User registered successfully");
                }
                else
                {
                    return BadRequest("User registration failed"); 
                }
                
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                // Log the exception (using a logger)
                return StatusCode(500, "An error occurred during registration");
            }
        }

        [HttpGet("{email}")]
        public IActionResult GetUser([FromQuery] string email)
        {

            try
            {
                var connectionString = _configuration.GetConnectionString("AzurePostgresConnection");

                // 2. Npgsql Connection Object
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                // 3. SQL Command Preparation
                var userexists = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE email = @email", connection);
                userexists.Parameters.AddWithValue("@email", email);
                int existingUserCount = (int)(long)userexists.ExecuteScalar();

                if (existingUserCount >0)
                {
                    var command =
                        new NpgsqlCommand(
                            "INSERT INTO login_history (email, activity_timestamp) VALUES (@email, @activity_timestamp)", connection);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@activity_timestamp",System.DateTime.Now);
                    var rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();

                return Ok("Login registered successfully");
            } catch (Exception ex)
            {
                
                // Log the exception (using a logger)
                return StatusCode(500, "An error occurred during registration");
            }

        }
    }
}