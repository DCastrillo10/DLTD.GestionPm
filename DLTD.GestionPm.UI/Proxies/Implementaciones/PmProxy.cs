using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Pm;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Pm;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class PmProxy : IPmProxy
    {
        private readonly HttpClient _httpClient;

        public PmProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<PaginationResponse<ListaPmResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaPmResponse>>($"api/Pm/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }
        public async Task<BaseResponse<PmResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<PmResponse>>($"api/Pm/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(PmRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Pm", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }
        public async Task<BaseResponse> Actualizar(int id, PmRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/Pm/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/Pm/{id}") ?? new();        
        }

        public async Task<BaseResponse<bool>> ExistePmAsync(int idTipoPm, int idModelo, string NoEquipo, string WorkOrder)
        {
            string url = $"api/Pm/existe?idTipoPm={idTipoPm}&idModelo={idModelo}&NoEquipo={NoEquipo}&WorkOrder={WorkOrder}";

            return await _httpClient.GetFromJsonAsync<BaseResponse<bool>>(url) ?? new();
        
        }

        public async Task<BaseResponse<IEnumerable<PmDetallesResponse>>> ObtenerTareasxModeloTipoPm(int idModelo, int idTipoPm)
        {
            string url = $"api/Pm/tareas?idModelo={idModelo}&idTipoPm={idTipoPm}";

            return await _httpClient.GetFromJsonAsync<BaseResponse<IEnumerable<PmDetallesResponse>>> (url) ?? new();
        }
    }
}
