using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoDemora;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class TipoDemoraProxy: ITipoDemoraProxy
    {
        private readonly HttpClient _httpClient;

        public TipoDemoraProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaTipoDemoraResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaTipoDemoraResponse>>($"api/TipoDemora/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<ICollection<ListaTipoDemoraResponse>>> Listarcombo()
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<ICollection<ListaTipoDemoraResponse>>>($"api/TipoDemora/Select") ?? new();
        }

        public async Task<BaseResponse> Registrar(TipoDemoraRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/TipoDemora", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, TipoDemoraRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/TipoDemora/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/TipoDemora/{id}") ?? new();
        }
    }
}
