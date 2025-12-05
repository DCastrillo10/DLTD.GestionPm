using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoPm;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoPm;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class TipoPmProxy: ITipoPmProxy
    {
        private readonly HttpClient _httpClient;

        public TipoPmProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaTipoPmResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaTipoPmResponse>>($"api/TipoPm/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<ICollection<ListaTipoPmResponse>>> Listarcombo()
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<ICollection<ListaTipoPmResponse>>>($"api/TipoPm/Select") ?? new();
        }

        public async Task<BaseResponse> Registrar(TipoPmRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/TipoPm", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, TipoPmRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/TipoPm/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/TipoPm/{id}") ?? new();
        }
    }
}
