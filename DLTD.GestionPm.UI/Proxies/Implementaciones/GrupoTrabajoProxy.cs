using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.GrupoTrabajo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.GrupoTrabajo;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class GrupoTrabajoProxy: IGrupoTrabajoProxy
    {
        private readonly HttpClient _httpClient;

        public GrupoTrabajoProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<ListaGrupoTrabajoResponse>> Listar(PaginationRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginationResponse<ListaGrupoTrabajoResponse>>($"api/GrupoTrabajo/Pagination?Filter={request.Filter}&Page={request.Page}&Rows={request.Rows}") ?? new();
        }        

        public async Task<BaseResponse> Registrar(GrupoTrabajoRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/GrupoTrabajo", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse>();

            if (!post.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Hubo un error al procesar la peticion: {response!.Message}");
            }
            return response ?? new();
        }

        public async Task<BaseResponse> Eliminar(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<BaseResponse>($"api/GrupoTrabajo/{id}") ?? new();
        }
    }
}
