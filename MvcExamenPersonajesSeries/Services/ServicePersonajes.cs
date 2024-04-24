using MvcExamenPersonajesSeries.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcExamenPersonajesSeries.Services
{
    public class ServicePersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServicePersonajes(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
        }

        public async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/personajes";
            List<Personaje> data = await this.CallApiAsync<List<Personaje>>(request);
            return data;
        }

        public async Task<List<Personaje>> GetPersonajesSeriesAsync(string serie)
        {
            string request = "api/personajes/personajesseries/" + serie;
            List<Personaje> data = await this.CallApiAsync<List<Personaje>>(request);
            return data;
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/personajes/series";
            List<string> data = await this.CallApiAsync<List<string>>(request);
            return data;
        }

        public async Task<Personaje> GetPersonajeAsync(int idpersonaje)
        {
            string request = "api/personajes/" + idpersonaje;
            Personaje data = await this.CallApiAsync<Personaje>(request);
            return data;
        }


        public async Task InsertPersonajeAsync(int idpersonaje, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/insertpersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = idpersonaje;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.Serie = serie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(int idpersonaje, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/updatepersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = idpersonaje;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.Serie = serie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/deletepersonaje/" + idpersonaje;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
