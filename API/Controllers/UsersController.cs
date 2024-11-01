using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        CocHupQuizDBContext context = new CocHupQuizDBContext();

        // GET: Get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Can not found user.");
            }

            return Ok(user);
        }

        [HttpGet("List")]
        public IActionResult GetUserList()
        {
            var users = context.Users.ToList();

            return Ok(users);
        }

        [HttpGet(Name = "Login/{username}/{password}")]
        public IActionResult GetUserById(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return NotFound("Can't get parameter!");
            }
            else
            {
                User u = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (u == null)
                {
                    return NotFound("User does not exist in database!");
                }
                else
                {
                    return Ok(u);
                }


            }
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto registrationDto)
        {
            if (registrationDto == null)
            {
                return BadRequest("Registration data is required.");
            }

            if (string.IsNullOrEmpty(registrationDto.Username) ||
                string.IsNullOrEmpty(registrationDto.Password) ||
                string.IsNullOrEmpty(registrationDto.FullName))
            {
                return BadRequest("All fields are required.");
            }

            // Check if user already exists
            if (context.Users.Any(u => u.Username == registrationDto.Username))
            {
                return Conflict("Email address already in use.");
            }

            var newUser = new User
            {
                Username = registrationDto.Username,
                Password = registrationDto.Password,
                FullName = registrationDto.FullName,
                BirthYear = registrationDto.BirthYear,
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { username = newUser.Username, password = newUser.Password }, newUser);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
        {
            if (forgetPasswordDto == null)
            {
                return BadRequest("Forget password data is required.");
            }

            if (string.IsNullOrEmpty(forgetPasswordDto.Username) || string.IsNullOrEmpty(forgetPasswordDto.NewPassword))
            {
                return BadRequest("Email address and new password are required.");
            }

            // Find the user by email address
            var user = context.Users.FirstOrDefault(u => u.Username == forgetPasswordDto.Username);
            if (user == null)
            {
                return NotFound("User does not exist.");
            }

            // Update the user's password (consider hashing the password before saving)
            user.Password = forgetPasswordDto.NewPassword;
            await context.SaveChangesAsync();

            return Ok("Password updated successfully.");
        }
    }
}
