using BlazorApp.Shared.Models;
using System.Threading.Tasks;

namespace BlazorApp.Client.Interfaces
{
    public interface IAuthDataServices
    {
        public Task<LoginResult> LoginAsync(UserLogin login);
        public Task<MenuResult> ObtenerMenu();

    }
}
