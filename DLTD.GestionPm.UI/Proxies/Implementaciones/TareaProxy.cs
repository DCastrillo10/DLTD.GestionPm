using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Tarea;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Tarea;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class TareaProxy: ITareaProxy
    {
        private readonly HttpClient _httpClient;

        public TareaProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaTareaResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaTareaResponse>>($"api/Tarea/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<TareaResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<TareaResponse>>($"api/Tarea/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(TareaRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Tarea", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, TareaRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/Tarea/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/Tarea/{id}") ?? new();
        }
    }
}
