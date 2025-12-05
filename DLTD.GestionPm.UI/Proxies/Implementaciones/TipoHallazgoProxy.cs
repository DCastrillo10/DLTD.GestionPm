using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoHallazgo;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class TipoHallazgoProxy: ITipoHallazgoProxy
    {
        private readonly HttpClient _httpClient;

        public TipoHallazgoProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaTipoHallazgoResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaTipoHallazgoResponse>>($"api/TipoHallazgo/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<ICollection<ListaTipoHallazgoResponse>>> Listarcombo()
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<ICollection<ListaTipoHallazgoResponse>>>($"api/TipoHallazgo/Select") ?? new();
        }

        public async Task<BaseResponse> Registrar(TipoHallazgoRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/TipoHallazgo", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, TipoHallazgoRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/TipoHallazgo/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/TipoHallazgo/{id}") ?? new();
        }
    }
}
