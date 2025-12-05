using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoActividad;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoActividad;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class TipoActividadProxy: ITipoActividadProxy
    {
        private readonly HttpClient _httpClient;

        public TipoActividadProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaTipoActividadResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaTipoActividadResponse>>($"api/TipoActividad/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<ICollection<ListaTipoActividadResponse>>> Listarcombo()
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<ICollection<ListaTipoActividadResponse>>>($"api/TipoActividad/Select") ?? new();
        }

        public async Task<BaseResponse> Registrar(TipoActividadRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/TipoActividad", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, TipoActividadRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/TipoActividad/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/TipoActividad/{id}") ?? new();
        }
    }
}
