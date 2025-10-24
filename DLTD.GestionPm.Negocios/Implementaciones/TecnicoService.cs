using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class TecnicoService: ITecnicoService
    {
        private readonly ITecnicoRepository _repository;
        private readonly IEmailService _email;

        public TecnicoService(ITecnicoRepository repository, IEmailService email)
        {
            _repository = repository;
            _email = email;
        }

        public async Task<BaseResponse> AddAsync(TecnicoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var tecnico = request.Adapt<Tecnico>();
                await _repository.CreateAsync(tecnico, request.NoIdentificacion, request.Clave);
                response.IsSuccess = true;
                response.Message = "Tecnico registrado correctamente.";
                await _email.SendEmail(tecnico.Email!, "Registro al Sistema Integrado de PM", CreateTemplate(request));
                    
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        private string CreateTemplate(TecnicoRequest request)
        {
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates/Email/Bienvenida.html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException("Template not found", templatePath);
            }

            var template = File.ReadAllText(templatePath);
            template = template.Replace("{{name}}", $"{request.Nombres} {request.Apellidos}");
            template = template.Replace("{{usuario}}", request.NoIdentificacion);
            template = template.Replace("{{contraseña}}", request.Clave);
            template = template.Replace("{{logo_url}}", $"");
            template = template.Replace("{{login_link}}", $"");

            return template;
        }
    }
}
