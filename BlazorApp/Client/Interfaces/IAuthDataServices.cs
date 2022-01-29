using BlazorApp.Client.Data;
using BlazorApp.Shared.Models;
using System.Threading.Tasks;

namespace BlazorApp.Client.Interfaces
{
    public interface IAuthDataServices
    {
        public Task<LoginResult> LoginAsync(UserLogin login);
        public Task<User> ObtenerUserPorTokenAsync(string authToken);
        MenuResult ObtenerMenu(UserMenu userMenu, string token);
    }
}
