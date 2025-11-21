using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class TecnicoProxy: ITecnicoProxy
    {
        private readonly HttpClient _httpClient;
        public TecnicoProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse> Registrar(TecnicoRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Tecnico", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {                
                throw new HttpRequestException($"Hubo un error al tratar de realizar el registro: {response!.Message}");
            }
            return  response ?? new();
        }
    }
}
