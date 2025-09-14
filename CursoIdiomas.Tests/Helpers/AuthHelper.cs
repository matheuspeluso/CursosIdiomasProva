using CursoIdiomas.Domain.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace CursoIdiomas.Tests.Helpers
{
    public class AuthHelper
    {
        public static async Task<string> ObterTokenAsync()
        {
            var clientAuth = new WebApplicationFactory<CursosIdiomas.Auth.API.Program>().CreateClient();

            var loginRequest = new AutenticarUsuarioRequest
            {
                Email = "admin@admin.com",
                Senha = "Admin@2025"
            };

            var loginJson = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );

            var loginResponse = await clientAuth.PostAsync("/api/Usuario/autenticar", loginJson);
            loginResponse.EnsureSuccessStatusCode();

            var content = await loginResponse.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(content).AccessToken;

            return token;
        }
    }
}
