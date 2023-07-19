using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.CodeAnalysis.Scripting;
using static ChatApp.Models.RegisterUser;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.User == null)
          {
              return Problem("Entity set 'ApplicationDbContext.User'  is null.");
          }
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        private readonly Dictionary<string, UserRegistrationRequest> _users = new Dictionary<string, UserRegistrationRequest>();

        [HttpPost("/api/register")]
        public IActionResult RegisterUser(UserRegistrationRequest request)
        {
            // Validate the request
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new UserRegistrationErrorResponse { Error = "All fields are required." });
            }

            // Check if the email is already registered
            if (_users.ContainsKey(request.Email))
            {
                return Conflict(new UserRegistrationErrorResponse { Error = "Email is already registered." });
            }

            // Hash the password securely before storing it
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Generate a unique user ID (you can implement your own logic here)
            int userId = GenerateUserId();

            // Save the user details (in this example, we'll use the email as the key)
            _users[request.Email] = new UserRegistrationRequest
            {
                Email = request.Email,
                Name = request.Name,
                Password = hashedPassword
            };

            // Return the success response
            var response = new UserRegistrationResponse
            {
                UserId = userId,
                Name = request.Name,
                Email = request.Email
            };

            return Ok(response);
        }

        private int GenerateUserId()
        {
            // Implement your own logic to generate a unique user ID (database auto-increment, GUID, etc.)
            // For simplicity, we'll just use a random number generator here.
            Random random = new Random();
            return random.Next(1000, 10000);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
