using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Modelo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Modelo;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class ModeloProxy: IModeloProxy
    {
        private readonly HttpClient _httpClient;

        public ModeloProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaModeloResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaModeloResponse>>($"api/Modelo/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<ModeloResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<ModeloResponse>>($"api/Modelo/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(ModeloRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Modelo", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, ModeloRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/Modelo/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/Modelo/{id}") ?? new();
        }
    }
}
