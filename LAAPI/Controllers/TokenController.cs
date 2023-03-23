

using LADomain;
using LAService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace LAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private AutorService _autorService { get; set; }

        private IConfiguration Configuration { get; set; }

        public TokenController(AutorService autorService, IConfiguration configuration)
        {
            _autorService = autorService;
            Configuration = configuration;
        }
        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Token([FromBody] TokenRequest request)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var autor = _autorService.AutenticarAutor(request.email);

            if (autor == null)
                return Unauthorized();

            return Ok(new
            {
                AccessToken = this.GenerateToken(autor)
            });

        }

        private string GenerateToken(Autor autor)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("sub", autor.Id.ToString()),
                new Claim("email", autor.Email),
                new Claim("nome", autor.Nome)
            };

            var key = Encoding.Default.GetBytes(this.Configuration["TokenSecret"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),

                Issuer = "autor-token",
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(securityToken);

            return tokenHandler.WriteToken(token);
        }
    }
    public record TokenRequest(
        [Required(ErrorMessage = "Email é obrigatório")] string email,
        [Required(ErrorMessage = "Nome é obrigatorio")] string nome
        );
        
}
