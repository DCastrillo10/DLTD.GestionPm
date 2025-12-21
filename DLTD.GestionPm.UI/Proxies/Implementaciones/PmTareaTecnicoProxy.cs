using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaTecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaTecnico;
using DLTD.GestionPm.Dto.Response.Tarea;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class PmTareaTecnicoProxy: IPmTareaTecnicoProxy
    {
        private readonly HttpClient _httpClient;

        public PmTareaTecnicoProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaPmTareaTecnicoResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaPmTareaTecnicoResponse>>($"api/PmTareaTecnico/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }

        public async Task<BaseResponse<PmTareaTecnicoResponse>> ObtenerPorId(int id)
        {
            return await _httpClient.GetFromJsonAsync<BaseResponse<PmTareaTecnicoResponse>>($"api/PmTareaTecnico/{id}") ?? new();
        }
        public async Task<BaseResponse> Registrar(PmTareaTecnicoRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/PmTareaTecnico", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Actualizar(int id, PmTareaTecnicoRequest request)
        {
            var post = await _httpClient.PutAsJsonAsync($"api/PmTareaTecnico/{id}", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/PmTareaTecnico/{id}") ?? new();
        }
    }
}
