using BlazorApp.Client.Auth;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Shared
{
    public class UserPanelBase : ComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] ILoginServices loginService { get; set; }

        public Modal modalInfoUsuario { get; set; }
        public Modal modalCerrarSesion { get; set; }
        [Parameter] public EventCallback EventoCerrarModal { get; set; }

        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Roles { get; set; }
        public string Correo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask).User;

            //if (user.Identity.IsAuthenticated)
            //{
            //    Usuario = user.Identity.Name;
            //    Nombre = user.Claims.FirstOrDefault(c => c.Type == nameof(User.username)).Value;
            //    Roles = user.Claims.FirstOrDefault(c => c.Type == nameof(User.role)).Value;
            //    Correo = user.Claims.FirstOrDefault(c => c.Type == "Correo").Value;
            //}

            await base.OnInitializedAsync();
        }

        protected async Task CerrarSesion()
        {
            //await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticatedLogout();
            await loginService.Logout();
            NavigationManager.NavigateTo("/login");
        }

        public void AbrirModal()
        {
            modalInfoUsuario.Show();
        }

        public void CerrarModal()
        {
            modalInfoUsuario.Hide();
        }
    }
}
