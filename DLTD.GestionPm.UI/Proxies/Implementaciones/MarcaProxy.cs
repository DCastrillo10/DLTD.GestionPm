using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Marca;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Marca;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class MarcaProxy: IMarcaProxy
    {
        private readonly HttpClient _httpClient;

        public MarcaProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaMarcaResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaMarcaResponse>>($"api/Marca/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }        

        public async Task<BaseResponse> Registrar(MarcaRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Marca", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, MarcaRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/Marca/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/Marca/{id}") ?? new();
        }
    }
}
