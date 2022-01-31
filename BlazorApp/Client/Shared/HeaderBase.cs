using Blazorise;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorApp.Client.Shared
{
    public class HeaderBase : ComponentBase
    {
        public UserPanel modalRef { get; set; }

        public async Task InformacionUsuario()
        {
            modalRef.AbrirModal();
            await Task.CompletedTask;
        }

        public void CerrarModal()
        {
            modalRef.CerrarModal();
        }
    }
}
