using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjetoIntegradorAPI.DTOs;

namespace ProjetoIntegradorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em :";
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody]UsuarioDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e=>e.Errors));
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.senha);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(GeraToken(model));

        }

        [HttpPost("login")]

        public async Task<ActionResult> Login ([FromBody]UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.senha, isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
                return Ok(GeraToken(usuario));
            else
            {
                ModelState.AddModelError("", "login invalido");
                return BadRequest(ModelState);
                    
            }

        }

        
        private UsuarioToken GeraToken(UsuarioDto usuario)
        {
            var claims = new[]
            {
                new Claim( JwtRegisteredClaimNames.UniqueName, usuario.Email),
                new Claim("meupet","pipoca"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            
            var credenciais = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var expiracao = _config["TokenConfiguration:ExpireHours"];
            var exp = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["TokenConfiguration:Issuer"],
                audience:_config["TokenConfiguration:Audience"],
                claims:claims,
                expires:exp,
                signingCredentials: credenciais);

            return new UsuarioToken()
            {
                Autenticado = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Exp = exp,
                Message = "bem vindo"
            };


        }




    }
}
