using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiNet6CursoUdemy.DTO;
using WebApiNet6CursoUdemy.Services;

namespace WebApiNet6CursoUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class LoginAPIController : ControllerBase
    {
        private readonly IServicioUsuarioAPI _servicioUsuarioApi;
        private readonly IConfiguration _configuration;
        public LoginAPIController(IServicioUsuarioAPI servicioUsuarioApi, IConfiguration configuration)
        {
            _servicioUsuarioApi = servicioUsuarioApi;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioApiDTO>> Login(LoginAPI usuarioLogin)
        {
            UsuarioAPI Usuario = null;
            Usuario = await AutenticarUsuarioAsync(usuarioLogin);
            if (Usuario == null)
            {
                throw new Exception("Credenciales no válidas");                                                                   
            }
            else
            {
                Usuario = GenerarTokenJWT(Usuario);
            }
            return Usuario.convertirDTO();
        }

        private async Task<UsuarioAPI> AutenticarUsuarioAsync(LoginAPI usuarioLogin)
        {
            UsuarioAPI usuarioAPI = await _servicioUsuarioApi.DameUsuario(usuarioLogin);
            return usuarioAPI;
        }

        private UsuarioAPI GenerarTokenJWT(UsuarioAPI usuarioAPI)
        {
            //CABECERA
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ClaveSecreta"]));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _Header = new JwtHeader(_signingCredentials);

            //CLAIMS
            var _Claims = new[]
            {
                new Claim("usuario",usuarioAPI.Usuario),
                new Claim("email", usuarioAPI.Email),
                new Claim(JwtRegisteredClaimNames.Email, usuarioAPI.Email)
            };

            //PAYLOAD
            var _Payload = new JwtPayload(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30)
                );

            //TOKEN
            var _Token = new JwtSecurityToken(_Header, _Payload);

            usuarioAPI.Token = new JwtSecurityTokenHandler().WriteToken( _Token );
            return usuarioAPI;
        }
    }
}
