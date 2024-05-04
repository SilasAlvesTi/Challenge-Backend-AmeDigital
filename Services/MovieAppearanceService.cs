using System.Net.Http.Headers;
using Planetas_StarWars.Models;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Planetas_StarWars.Services.Exceptions;

namespace Planetas_StarWars.Services
{
    public class MovieAppearanceService
    {
        private readonly HttpClient _httpClient;

        public MovieAppearanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetMovieAppearanceTimesAsync(string planetName)
        {
            var response = await DataFromExternalApi("https://swapi.dev/api/planets/");

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                // TODO: estudar em que casos usar dynamic
                dynamic json = JObject.Parse(jsonStr);
                
                while (json.next != null)
                {
                    foreach (var data in json.results)
                    {
                        if (data.name == planetName)
                        {
                            var films = data.films;
                            return films.Count;
                        }
                    }
                    response = await DataFromExternalApi(json.next.ToString());
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new APIRequestException("Falha na requisição para a API externa");
                    }
                    jsonStr = await response.Content.ReadAsStringAsync();
                    json = JObject.Parse(jsonStr);
                }
            }

            throw new APIRequestException("Filme não é um filme válido");
        }

        public async Task<HttpResponseMessage> DataFromExternalApi(string url)
        {
            HttpClient _httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await _httpClient.SendAsync(request);
        }
    }
}
