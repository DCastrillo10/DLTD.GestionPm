using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Ruta;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Ruta;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class RutaProxy: IRutaProxy
    {
        private readonly HttpClient _httpClient;

        public RutaProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaRutaResponse>> Listar(PaginationRequest request)
        {            
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaRutaResponse>>($"api/Ruta/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }
        public async Task<BaseResponse<ICollection<ListaRutaResponse>>> Listarcombo()
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<ICollection<ListaRutaResponse>>>($"api/Ruta/Select") ?? new();
        }
        public async Task<BaseResponse> Registrar(RutaRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Ruta", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, RutaRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/Ruta/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/Ruta/{id}") ?? new();
        }
    }
}
