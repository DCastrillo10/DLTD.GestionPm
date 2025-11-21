using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using System.Net.Http.Json;

namespace DLTD.GestionPm.UI.Proxies.Implementaciones
{
    public class SecurityProxy: ISecurityProxy
    {
        private readonly HttpClient _httpClient;
        public SecurityProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest request)
        {
            var post = await _httpClient.PostAsJsonAsync("api/Users/Login", request);
            var response = await post.Content.ReadFromJsonAsync<BaseResponse<LoginResponse>>();

            if (!post.IsSuccessStatusCode)
            {                
                throw new HttpRequestException($"Hubo un error al iniciar sesión: {response!.Message}");
            }
            return  response ?? new();
        }
    }
}
