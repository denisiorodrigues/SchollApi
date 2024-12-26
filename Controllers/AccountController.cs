using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchollApi;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;

        public AccountController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null || model.PasswordsNotMatch())
            {
                ModelState.AddModelError("Password", "As senhas não conferem");
                return BadRequest(ModelState);
            }

            var result = await _authenticate.Register(model.Email, model.Password);

            if (result)
            {
                return Ok($"Usuário {model.Email} registrado com sucesso");
            }

            return BadRequest("Erro ao registrar usuário");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Usuário ou senha inválidos");
            }

            var result = await _authenticate.Autenticate(model.Email, model.Password);

            if (result)
            {
                return Ok(GenerateJwtToken(model));
            }

            ModelState.AddModelError("Password", "Usuário ou senha inválidos");
            return BadRequest(ModelState);
        }

        private ActionResult<UserToken> GenerateJwtToken(LoginModel model)
        {
            var claims = new[]
            {
                new Claim("email", model.Email),
                new Claim("myToken", "token aqui"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddMinutes(20);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience:  _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return Ok(new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }
    }
}
