namespace BlazorApp.Shared.Models
{
    public class User
    {
        public int userid { get; set; }
        public string username { get; set; }

        public string matricula { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
