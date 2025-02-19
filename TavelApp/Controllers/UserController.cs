using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using TavelApp.Models;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserRegistration userData)
        {
            if (userData == null || string.IsNullOrEmpty(userData.email) || string.IsNullOrEmpty(userData.name))
            {
                return BadRequest("Invalid user data.");
            }

            var connectionString = _configuration.GetConnectionString("LocalDBConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the user already exists
                    using (var userExistsCmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE email = @email", connection))
                    {
                        userExistsCmd.Parameters.AddWithValue("@email", userData.email);
                        int existingUserCount = Convert.ToInt32(userExistsCmd.ExecuteScalar());

                        if (existingUserCount > 0)
                        {
                            return Ok("User Exists");
                        }
                    }

                    // Insert new user
                    using (var command = new SqlCommand("INSERT INTO users (name, email, picture) VALUES (@username, @email, @picture)", connection))
                    {
                        command.Parameters.AddWithValue("@username", userData.name);
                        command.Parameters.AddWithValue("@email", userData.email);
                        command.Parameters.AddWithValue("@picture", userData.picture);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0 ? Ok("User registered successfully") : BadRequest("User registration failed");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (use proper logging mechanism)
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
        }

        [HttpGet("{email}")]
        public IActionResult GetUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var connectionString = _configuration.GetConnectionString("LocalDBConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the user exists
                    using (var userExistsCmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE email = @email", connection))
                    {
                        userExistsCmd.Parameters.AddWithValue("@email", email);
                        int existingUserCount = Convert.ToInt32(userExistsCmd.ExecuteScalar());

                        if (existingUserCount > 0)
                        {
                            // Insert login activity
                            using (var command = new SqlCommand("INSERT INTO login_history (email, activity_timestamp) VALUES (@email, @activity_timestamp)", connection))
                            {
                                command.Parameters.AddWithValue("@email", email);
                                command.Parameters.AddWithValue("@activity_timestamp", DateTime.Now);
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    return Ok("Login registered successfully");
                }
                catch (Exception ex)
                {
                    // Log the exception (use proper logging mechanism)
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
