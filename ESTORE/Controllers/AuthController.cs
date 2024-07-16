using ESTORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ESTORE.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;    
        private readonly SignInManager<IdentityUser> _signInManager;    
        private  readonly RoleManager<IdentityRole> _roleManager;   

        public AuthController(IConfiguration configuration, SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager; 
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            if (user == null)
            {
                return BadRequest("User not exists");
            }
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, false);
            if (!result.Succeeded) {
                return Unauthorized();
            }
            var getUserRoles = await _userManager.GetRolesAsync(user);
            var token = this.GenerateJwtToken(user.Id, userLogin.Email, getUserRoles);
            return Ok(new { token });
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(string username, string password)
        {
            var user = new IdentityUser {  UserName  = username, Email = username };            

            var createdUser = await _userManager.CreateAsync(user, password);

            if (!createdUser.Succeeded)
            {
                return BadRequest(createdUser.Errors);
            }

            var USER = "User";
            var role = await _roleManager.FindByNameAsync(USER);
            if (role == null)
            {
                var roleIdentity = new IdentityRole { Name = USER };
                var roleResult = await _roleManager.CreateAsync(roleIdentity);
            }

            var roles = new[] { USER };

            var addRoleResult = await _userManager.AddToRolesAsync(user, roles);

           if (!addRoleResult.Succeeded) {
                return BadRequest("Adding Role to user failed");
            }


            return Ok("You are registered successfully");
        }

        //what http verb or attributes, can we create custome attrubute, how?


        [Authorize]
        [HttpGet("securePath")]  
        public object test()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return new
            {
                success = true,
                userId,
                userEmail
            };
        }


        private string GenerateJwtToken(string uid, string username, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, uid)
            };

            foreach (var role in roles)
            {

                claims.Add(new Claim("Roles", role));
            }

            var jwtKey = _configuration["Jwt:Key"];
            if(jwtKey == null)
            {
                throw new Exception("Key required");
            }
            var keyInByte = Encoding.UTF8.GetBytes(jwtKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyInByte);
            var siningCredetials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: siningCredetials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
