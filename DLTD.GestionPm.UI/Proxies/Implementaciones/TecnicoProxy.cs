using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Request.Marca;
using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;
using DLTD.GestionPm.Dto.Response.Marca;
using DLTD.GestionPm.Dto.Response.Modelo;
using DLTD.GestionPm.Dto.Response.Tecnico;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class TecnicoProxy: ITecnicoProxy
    {
        private readonly HttpClient _httpClient;
        public TecnicoProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaTecnicoResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaTecnicoResponse>>($"api/Tecnico/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<TecnicoResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<TecnicoResponse>>($"api/Tecnico/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(TecnicoRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Tecnico", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {                
                throw new HttpRequestException($"Hubo un error al tratar de realizar el registro: {response!.Message}");
            }
            return  response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, TecnicoRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/Tecnico/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/Tecnico/{id}") ?? new();
        }
    }
}
