using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Maquina;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Maquina;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class MaquinaProxy: IMaquinaProxy
    {
        private readonly HttpClient _httpClient;

        public MaquinaProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaMaquinaResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaMaquinaResponse>>($"api/Maquina/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<MaquinaResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<MaquinaResponse>>($"api/Maquina/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(MaquinaRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Maquina", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, MaquinaRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/Maquina/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/Maquina/{id}") ?? new();
        }
    }
}
