using BlazorApp.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazored.Modal;
using BlazorApp.Client.Interfaces;
using BlazorApp.Client.Services;
using BlazorApp.Client.Data;

namespace BlazorApp.Client
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<JwtAuthenticatorProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticatorProvider>(provider => provider.GetRequiredService<JwtAuthenticatorProvider>());
            builder.Services.AddScoped<ILoginServices, JwtAuthenticatorProvider>(provider => provider.GetRequiredService<JwtAuthenticatorProvider>());
            builder.Services.AddBlazoredModal();
            builder.Services.AddScoped<IAuthDataServices, AuthDataServices>();


            //System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            await builder.Build().RunAsync();


        }

    }
}
