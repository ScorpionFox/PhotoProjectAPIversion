using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using PhotoProjectAPI.Data;
using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.Dataset.VM.UserManagment;
using PhotoProjectAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IConfiguration _configuration;

        public UserManagmentController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IConfiguration configuration, TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
        }

        //Tokeny
        [HttpPost("Token-refresh")]
        public async Task<IActionResult> TokenRefreshment([FromForm] RequestForTokenViewmodel request)
        {
            try
            {
                var result = await VerifyAndRefreshTokenAsync(request);

                if (result == null)
                {
                    return BadRequest("Wrong tokens!");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<UserManagmentResultViewmodel> TokenGeneratorAsync(User user, string existingToken)
        {
            string Role = "USER";
            if (user.UserName == "admin")
            {
                Role = "ADMIN";
            }

            var authClaims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Role, Role),
        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            var jwtSecurityToken = new JwtSecurityTokenHandler().WriteToken(token);
            var tokenRefreshment = new TokenRefreshment();

            if (string.IsNullOrEmpty(existingToken))
            {
                tokenRefreshment = new TokenRefreshment()
                {
                    IdJWT = token.Id,
                    IsExpired = false,
                    UserId = user.Id,
                    AddingDate = DateTime.Now,
                    ExpiringDate = DateTime.Now.AddMonths(4),
                    Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
                };

                await _context.TokenRefreshments.AddAsync(tokenRefreshment);
                await _context.SaveChangesAsync();
            }

            var response = new UserManagmentResultViewmodel()
            {
                Token = jwtSecurityToken,
                TokenRefreshment = (string.IsNullOrEmpty(existingToken)) ? tokenRefreshment.Token : existingToken,
                ExpiringDate = token.ValidTo
            };

            return response;
        }

        private async Task<UserManagmentResultViewmodel> VerifyAndRefreshTokenAsync(RequestForTokenViewmodel request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Weryfikacja formatu JWT
                var tokenInVerification = jwtTokenHandler.ValidateToken(request.Token, _tokenValidationParameters, out var validatedToken);

                // Sprawdzenie algorytmu szyfrowania
                if (validatedToken is JwtSecurityToken jwtSecurityToken && !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("Wrong cryptographic algorithm");
                }

                // Weryfikacja daty ważności tokena
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = UnixTimeStampToDateTimeInUTC(utcExpireDate);

                if (expireDate > DateTime.UtcNow)
                {
                    throw new Exception("The token has not reached its expiration");
                }

                // Sprawdzenie czy token istnieje w bazie danych
                var dbRefreshToken = await _context.TokenRefreshments.FirstOrDefaultAsync(n => n.Token == request.TokenRefreshment);

                if (dbRefreshToken == null)
                {
                    throw new Exception("No matching refresh token found in the database");
                }

                // Weryfikacja ID tokenu
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (dbRefreshToken.IdJWT != jti)
                {
                    throw new Exception("Token doesn't match");
                }

                // Weryfikacja daty ważności tokenu
                if (dbRefreshToken.ExpiringDate <= DateTime.UtcNow)
                {
                    throw new Exception("Your refresh token has expired. Please log in again.");
                }

                // Sprawdzenie, czy token jest wycofany
                if (dbRefreshToken.IsExpired)
                {
                    throw new Exception("Refresh token has been invalidated.");
                }

                // Generowanie nowego tokenu na podstawie istniejącego refresh tokenu
                var dbUser = await _userManager.FindByIdAsync(dbRefreshToken.UserId);
                var newTokenResponse = await TokenGeneratorAsync(dbUser, request.TokenRefreshment);

                return newTokenResponse;
            }
            catch (SecurityTokenExpiredException)
            {
                var dbRefreshToken = await _context.TokenRefreshments.FirstOrDefaultAsync(n => n.Token == request.TokenRefreshment);

                // Generowanie nowego tokenu na podstawie istniejącego refresh tokenu
                var dbUser = await _userManager.FindByIdAsync(dbRefreshToken.UserId);
                var newTokenResponse = await TokenGeneratorAsync(dbUser, request.TokenRefreshment);

                return newTokenResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        private DateTime UnixTimeStampToDateTimeInUTC(long utcExpireDate)
        {
            var dateTimeVal = new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(utcExpireDate);
            return dateTimeVal;
        }

        //Użytkownicy
        [HttpPost("User-Register")]
        public async Task<IActionResult> Registration([FromForm] UserRegistrationViewmodel registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure all necessary information is provided");
            }

            var userTaken = await _userManager.FindByNameAsync(registration.Login);

            if (userTaken != null)
            {
                return BadRequest($"The username '{registration.Login}' is already in use. Please choose a different one.");
            }

            var newUser = AddUser(registration);

            var result = await _userManager.CreateAsync(newUser, registration.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Unable to add the user account. Please check your registration details and try again.");
            }

            return Created(nameof(Registration), $"User '{registration.Login}' has been added succesfully.");
        }

        private User AddUser(UserRegistrationViewmodel registration)
        {
            return new User()
            {
                UserName = registration.Login,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
        }

        [HttpPost("User-LogIn")]
        public async Task<IActionResult> Login([FromForm] UserLoginViewmodel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure all necessary information is provided");
            }

            var user = await _userManager.FindByNameAsync(login.Login);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Unauthorized();
            }

            var tokenValue = await TokenGeneratorAsync(user, "");
            return Ok(tokenValue);

        }      
    }
}
