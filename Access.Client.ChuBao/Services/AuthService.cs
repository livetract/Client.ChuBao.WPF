using Core.Client.ChuBao.Dtos;
using Core.Client.ChuBao.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Access.Client.ChuBao.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        public AuthService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<LoginResult> Login(LoginDto model)
        {
            var url = _client.BaseAddress + "login";
            var request = await _client.PostAsJsonAsync(url, model,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

            if (!request.IsSuccessStatusCode)
            {
                return new LoginResult() { IsLogin = false, Token = string.Empty};
            }
            //var reponse = await request.Content.ReadAsStringAsync(); // 这个不能识别
            var token = await request.Content.ReadFromJsonAsync<string>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return new LoginResult() { IsLogin = true, Token = token };
        }
    }
}
