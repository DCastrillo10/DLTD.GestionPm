using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.CheckList;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.CheckList;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class CheckListProxy : ICheckListProxy
    {
        private readonly HttpClient _httpClient;

        public CheckListProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaCheckListResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaCheckListResponse>>($"api/CheckList/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<CheckListResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<CheckListResponse>>($"api/CheckList/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(CheckListRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/CheckList", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }
        public async Task<BaseResponse> Actualizar(int id, CheckListRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/CheckList/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/CheckList/{id}") ?? new();
        }
    }
}
