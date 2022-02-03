using BlazorApp.Client.Helpers;
using BlazorApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Auth
{
    public class JwtAuthenticatorProvider : AuthenticationStateProvider, ILoginServices
    {
        private readonly IJSExtensions js;
        private readonly HttpClient httpClient;
        public static readonly string TOKENKEY = "TOKENKEY";
        private AuthenticationState anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public JwtAuthenticatorProvider(IJSRuntime _js, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            js = new IJSExtensions(_js);
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return anonimo;
            }

            return BuildAuthenticationState(token);
        }

        public async Task Login(string token)
        {
            await js.RemoveItem(TOKENKEY);
            await js.SetInLocalStorage(TOKENKEY, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
            await js.RemoveItem(TOKENKEY);
            NotifyAuthenticationStateChanged(Task.FromResult(anonimo));
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
            //return new AuthenticationState(new ClaimsPrincipal(GetClaimsIdentity(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(Usuario user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.correo != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(nameof(Usuario.userid), user.userid.ToString()),
                                    new Claim(nameof(Usuario.username), user.username),
                                    new Claim(nameof(Usuario.matricula), user.matricula),
                                    new Claim(ClaimTypes.Name, user.nombre),
                                    new Claim(nameof(Usuario.role), user.role),
                                    new Claim("Correo", user.correo)
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var stream = jwt;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object role);

            if (role != null)
            {
                if (role.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(role.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
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
