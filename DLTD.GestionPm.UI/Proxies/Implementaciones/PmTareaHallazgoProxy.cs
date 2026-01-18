using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaHallazgo;
using DLTD.GestionPm.Dto.Response.Tarea;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class PmTareaHallazgoProxy: IPmTareaHallazgoProxy
    {
        private readonly HttpClient _httpClient;

        public PmTareaHallazgoProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaPmTareaHallazgoResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaPmTareaHallazgoResponse>>($"api/PmTareaHallazgo/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }
        public async Task<BaseResponse<PmTareaHallazgoResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<PmTareaHallazgoResponse>>($"api/PmTareaHallazgo/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(PmTareaHallazgoRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/PmTareaHallazgo", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }
        
        public async Task<BaseResponse> Actualizar(int id, PmTareaHallazgoRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/PmTareaHallazgo/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/PmTareaHallazgo/{id}") ?? new();
        }


        public async Task<BaseResponse<string>> ObtenerEquipo(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<string>>($"api/PmTareaHallazgo/equipo/{id}") ?? new();
        }
    }
}
