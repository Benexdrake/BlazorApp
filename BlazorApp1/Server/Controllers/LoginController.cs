using Discord.WebSocket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorApp1.Server.Controllers
{
    [Route("User")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetToken")]
        [Authorize(AuthenticationSchemes = "Discord")]
        public object GetToken()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string key = _config.GetValue<string>("Jwt:EncryptionKey");
            string issuer = _config.GetValue<string>("Jwt:Issuer");
            string audience = _config.GetValue<string>("Jwt:Audience");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("discordId", userId));

            var token = new JwtSecurityToken(
                issuer,
                audience,
                permClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
             );

            var jwt_Token = new JwtSecurityTokenHandler().WriteToken(token);

            return new
            {
                ApiToken = jwt_Token,
            };
        }

        [HttpGet("GetUserID")]
        [Authorize(AuthenticationSchemes = "Discord")]
        public async Task<ActionResult> GetUserId()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
            if(userId is not null)
                return Ok(userId);
            return BadRequest();
        }

        [HttpGet("GetUser")]
        [Authorize(AuthenticationSchemes = "Discord")]
        public async Task<ActionResult> GetUser(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
            {
                userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
            }

            var id = ulong.Parse(userId);
            DiscordSocketClient client = new DiscordSocketClient();
            client.LoginAsync(Discord.TokenType.Bot, _config["Discord:Token"]);
            var user = await client.GetUserAsync(id);
            if(user is not null)
                return Ok(user);
            return BadRequest();
        }
    }
}
