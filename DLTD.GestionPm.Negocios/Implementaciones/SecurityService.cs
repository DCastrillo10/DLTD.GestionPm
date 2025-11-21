using DLTD.GestionPm.AccesoDatos.Configuracion;
using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Request.Security;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class SecurityService: ISecurityService
    {
        private readonly UserManager<SecurityEntity> _userManager;
        private readonly JwtSettings _jwt;
        private readonly ILogger<TecnicoService> _logger;

        public SecurityService(UserManager<SecurityEntity> userManager, IOptions<JwtSettings> jwtOptios, ILogger<TecnicoService> logger)
        {
            _userManager = userManager;
            _jwt = jwtOptios.Value;
            _logger = logger;
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest request)
        {
            var response = new BaseResponse<LoginResponse>();
            try
            {
                var usuario = await _userManager.FindByNameAsync(request.UserName);
                if (usuario == null) throw new InvalidDataException($"El usuario {request.UserName} no existe.");
                
                var clave = await _userManager.CheckPasswordAsync(usuario, request.Password);
                if (!clave) throw new InvalidDataException("La contraseña es invalida.");

                var roles = await _userManager.GetRolesAsync(usuario);
                var rol = roles.First();

                var token = GenerateToken(usuario, rol);

                response.IsSuccess = true;
                response.Result = new()
                {
                    Token = token,
                    Nombre = usuario.NombresCompletos!,
                    Rol = rol
                };
                response.Message = "Inicio de sesion correcto.";

            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al iniciar sesion.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        private string GenerateToken(SecurityEntity request, string role)
        {
            var dateExpiration = DateTime.Now.AddMinutes(_jwt.ExpirationMinutes);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.NombresCompletos!),
                new Claim(ClaimTypes.Email, request.Email!),
                new Claim(ClaimTypes.Expiration, dateExpiration.ToLongDateString()!),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, request.IdUsuario.ToString()!)
            };

            var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var credentials = new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(
                _jwt.Issuer,
                _jwt.Audience,
                claims,
                DateTime.Now,
                dateExpiration
            );

            var jwtToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
