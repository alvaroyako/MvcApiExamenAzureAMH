using MvcApiExamenAzureAMH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcApiExamenAzureAMH.Services
{
    public class ServiceApiSeries
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiSeries(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
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

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/Series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/Personajes";
            List<Personaje> pjs = await this.CallApiAsync<List<Personaje>>(request);
            return pjs;
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/series/" + idserie;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task<List<Personaje>> FindPersonajesSerieAsync(int idserie)
        {
            string request = "/api/series/PersonajesSerie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task InsertPersonajeAsync(string nombre, string imagen,int idSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje pj = new Personaje();
                pj.IdPersonaje = 0;
                pj.Nombre = nombre;
                pj.Imagen = imagen;
                pj.IdSerie = idSerie;
                string json = JsonConvert.SerializeObject(pj);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdateSerieAsync(int id, string nombre, string imagen, double puntuacion, int año)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Series/";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie ser = new Serie { IdSerie = id, Nombre = nombre, Imagen = imagen, Puntuacion=puntuacion,Año=año };
                string json = JsonConvert.SerializeObject(ser);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/" + idpersonaje;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task UpdatePersonajeSerieAsync(int idpersonaje, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/UpdatePersonajeSerie/"+idpersonaje+"/"+idserie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = "";
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }


    }
}
