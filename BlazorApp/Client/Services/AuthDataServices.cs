using BlazorApp.Client.Interfaces;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp.Shared.Models;
using BlazorApp.Client.Helpers;

namespace BlazorApp.Client.Services
{
    public class AuthDataServices : IAuthDataServices
    {
        private HttpClient httpClient { get; }
        private AppSettings appSettings { get; }

        public AuthDataServices(HttpClient httpClient, IOptions<AppSettings> options)
        {
            appSettings = options.Value;

            httpClient.BaseAddress = new Uri("http://localhost:8081/Api/");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorAssembly");

            this.httpClient = httpClient;
        }

        public async Task<LoginResult> LoginAsync(UserLogin login)
        {
            try
            {
                string loginAsJson = JsonSerializer.Serialize(login);
                HttpResponseMessage response = await httpClient.PostAsync("Login/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
                LoginResult loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return loginResult;
            }
            catch (Exception e)
            {
                
                throw;
            }

        }
        public async Task<MenuResult> ObtenerMenu()
        {
            MenuRequest menurequest = new MenuRequest();
            menurequest.CodigoApp = "CIS";
            menurequest.UserId = "1";
            try
            {
                string loginAsJson = JsonSerializer.Serialize(menurequest);
                HttpResponseMessage response = await httpClient.PostAsync("Login/Menu", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
                MenuResult menuresult = JsonSerializer.Deserialize<MenuResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return menuresult;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
