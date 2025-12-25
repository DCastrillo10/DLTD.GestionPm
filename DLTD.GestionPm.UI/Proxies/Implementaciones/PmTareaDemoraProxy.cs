using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaDemora;
using DLTD.GestionPm.Dto.Response.Tarea;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class PmTareaDemoraProxy: IPmTareaDemoraProxy
    {
        private readonly HttpClient _httpClient;

        public PmTareaDemoraProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaPmTareaDemoraResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaPmTareaDemoraResponse>>($"api/PmTareaDemora/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }
        public async Task<BaseResponse<PmTareaDemoraResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<PmTareaDemoraResponse>>($"api/PmTareaDemora/{id}") ?? new();
        }
        public async Task<BaseResponse<ICollection<PmTareaDemoraResponse>>> HistorialActividades(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<ICollection<PmTareaDemoraResponse>>>($"api/PmTareaDemora/historialactividades/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(PmTareaDemoraRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/PmTareaDemora", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }
        
        public async Task<BaseResponse> Actualizar(int id, PmTareaDemoraRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/PmTareaDemora/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/PmTareaDemora/{id}") ?? new();
        }
    }
}
