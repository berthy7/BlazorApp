using BlazorApp.Client.Auth;
using BlazorApp.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Base
{
    public abstract class FODataServices
    {
        /// <summary>
        /// Variable para las solicitudes HTTP.
        /// </summary>
        protected HttpClient httpClient { get; set; }
        /// <summary>
        /// Variable en donde se recupera las rutas para las solicitudes HTTP, configuradas en el appsettings.
        /// </summary>
        protected AppSettings appSettings { get; set; }
        /// <summary>
        /// Variables para recuperar información.
        /// </summary>
        //protected ProtectedLocalStorage localStorageService { get; set; }
        /// <summary>
        /// Variable para el nombre de la API a la cual se va a realizar las solicitudes HTTP.
        /// </summary>
        protected string urlApi { get; set; }
        /// <summary>
        /// Método para setear el token de autorización al HTTPClient.
        /// </summary>
        /// <returns></returns>
        //protected async Task SetToken()
        //{
        //    var token = await localStorageService.GetAsync<string>("accessToken");
        //    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //}
        /// <summary>
        /// Método para realizar una solicitud HTTP de tipo SEND, en el cual los parametros de busqueda se envian como un objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto de envio y recepción.</typeparam>
        /// <param name="methopApi">Nombre del método API a realizar la solicitud HTTP.</param>
        /// <param name="obj">Objeto de envío para realizar la solicitud HTTP.</param>
        /// <returns>Retorna una lista.</returns>
        protected async Task<List<T>> Search<T>(string methopApi, object obj)
        {
            List<T> objs = new List<T>();
            try
            {
                //await SetToken();

                HttpRequestMessage requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{appSettings.UrlNegocio}{urlApi}/{methopApi}"),
                    Content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                    objs = JsonSerializer.Deserialize<List<T>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return objs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<List<T>> SearchForArray<T, P>(string methopApi, P[] list)
        {
            List<T> objs = new List<T>();
            try
            {

                HttpRequestMessage requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{appSettings.UrlNegocio}{urlApi}/{methopApi}"),
                    Content = new StringContent(JsonSerializer.Serialize(list), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                    objs = JsonSerializer.Deserialize<List<T>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return objs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Método para realizar una solicitud HTTP de tipo POST, en el cual los parametros se envian como un objeto.
        /// </summary>
        /// <param name="obj">Objeto de envío para realizar la solicitud HTTP.</param>
        /// <returns>Retorna un true (si la solicitud fue exitosa) y false (si la solicitud no se proceso).</returns>
        protected async Task<T> Save<T>(T obj)
        {
            //await SetToken();

            StringContent content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"{urlApi}", content);

            if (response.IsSuccessStatusCode)
                obj = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return obj;
        }
        /// <summary>
        /// Método para realizar una solicitud HTTP de tipo PUT, en el cual los parametros se envian como un objeto.
        /// </summary>
        /// <param name="obj">Objeto de envío para realizar la solicitud HTTP.</param>
        /// <returns>Retorna un true (si la solicitud fue exitosa) y false (si la solicitud no se proceso).</returns>
        protected async Task<bool> Modify(object obj)
        {
            //await SetToken();

            StringContent content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync($"{urlApi}", content);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
        /// <summary>
        /// Método para realizar una solicitud HTTP de tipo DELETE.
        /// </summary>
        /// <param name="id">Valor de la llave primaria del registro.</param>
        /// <returns>Retorna un true (si la solicitud fue exitosa) y false (si la solicitud no se proceso).</returns>
        protected async Task<bool> Delete(int id)
        {
            //await SetToken();

            HttpResponseMessage response = await httpClient.DeleteAsync($"{urlApi}/" + id);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        protected async Task<bool> DeleteIntermedia(int id, int id2)
        {
            //await SetToken();

            HttpResponseMessage response = await httpClient.DeleteAsync($"{urlApi}/" + id + "," + id2);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
        /// <summary>
        /// Método para realizar una solicitud HTTP de tipo GET, para recuperar un objeto en específico.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para la recepción</typeparam>
        /// <param name="id">Valor de la llave primaria del registro.</param>
        /// <returns>Retorna un objeto.</returns>
        protected async Task<T> ObtainId<T>(int id) where T : new()
        {
            //await SetToken();

            T obj = new T();

            HttpResponseMessage response = await httpClient.GetAsync($"{urlApi}/" + id);

            if (response.IsSuccessStatusCode)
                obj = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return obj;
        }
        /// <summary>
        ///  Método para realizar una solicitud HTTP de tipo GET, en el cual pasamos un valor numerico.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto de envio y recepción.</typeparam>
        /// <param name="methopApi">Nombre del método API a realizar la solicitud HTTP.</param>
        /// <param name="id">Valor de la llave primaria del registro.</param>
        /// <returns>Retorna una lista.</returns>
        protected async Task<List<T>> ObtainId<T>(string methopApi, int id)
        {
            List<T> objs = new List<T>();
            try
            {
                //await SetToken();
                HttpResponseMessage response = await httpClient.GetAsync($"{urlApi}/{methopApi}/" + id);

                if (response.IsSuccessStatusCode)
                    objs = JsonSerializer.Deserialize<List<T>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return objs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Método para realizar una solicitud HTTP de tipo GET.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para la recepció</typeparam>
        /// <returns>Retorna una lista.</returns>
        protected async Task<List<T>> All<T>()
        {
            //await SetToken();

            List<T> obj = new List<T>();

            HttpResponseMessage response = await httpClient.GetAsync($"{urlApi}");

            if (response.IsSuccessStatusCode)
                obj = JsonSerializer.Deserialize<List<T>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return obj;
        }

        protected async Task<List<T>> ObtainForString<T>(string service)
        {
            //await SetToken();
            List<T> Listobj = new List<T>();

            HttpResponseMessage response = await httpClient.GetAsync($"{urlApi}/" + service);

            if (response.IsSuccessStatusCode)
                Listobj = JsonSerializer.Deserialize<List<T>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Listobj;

        }

        //protected async Task<List<T>> ObtainforObject<T>(string service, FiltroConsulta obj)
        //{
        //    //await SetToken();
        //    List<T> Listobj = new List<T>();

        //    StringContent content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await httpClient.PostAsync($"{urlApi}/" + service, content);

        //    if (response.IsSuccessStatusCode)
        //        Listobj = JsonSerializer.Deserialize<List<T>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //    return Listobj;
        //}
    }
}
