using Blazored.SessionStorage;
using DLTD.GestionPm.Dto.Response.Login;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DLTD.GestionPm.UI.Services
{
    public class AuthService : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly ClaimsPrincipal _claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly HttpClient _httpClient;

        public AuthService(ISessionStorageService sessionStorageService, HttpClient httpClient)
        {
            _sessionStorageService = sessionStorageService; 
            _httpClient = httpClient; 
        }

        public async Task AuthenticationAsync(LoginResponse request)
        {
            if (request != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.Token);
                await _sessionStorageService.SetItemAsync("session", request);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }        

        public async Task Logout()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            await _sessionStorageService.RemoveItemAsync("session");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<bool> IsActiveSession()
        {
            var session = await _sessionStorageService.GetItemAsync<LoginResponse>("session");
            return session == null ? false : true;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var session = await _sessionStorageService.GetItemAsync<LoginResponse>("session");

            if (session is null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return new AuthenticationState(_claimsPrincipal);
            }

            if(_httpClient.DefaultRequestHeaders.Authorization == null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.Token);
            }

            var jwtData = ReadToken(session.Token);
            var claims = new ClaimsPrincipal(new ClaimsIdentity(jwtData.Claims, "jwt"));
            return new AuthenticationState(claims);
        }

        private JwtSecurityToken ReadToken(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(Token);
        }
    }
}
