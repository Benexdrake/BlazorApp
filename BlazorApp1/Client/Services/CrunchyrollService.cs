using BlazorApp1.Shared.Models.Crunchyroll;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace BlazorApp1.Client.Services
{
    
    public class CrunchyrollService : ICrunchyrollService
    {
        private readonly HttpClient _http;

        public CrunchyrollService(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<List<Anime>> GetAllAnimes()
        {
            var animes = await _http.GetFromJsonAsync<List<Anime>>("api/crunchyroll/all");
            return animes;
        }

        public async Task<List<Anime>> GetAllAnimes(int max)
        {
            var animes = await _http.GetFromJsonAsync<List<Anime>>($"api/crunchyroll/all?max={max}");
            return animes;
        }

        public async Task<Anime> GetAnimeByName(string name)
        {
            var anime = await _http.GetFromJsonAsync<Anime>($"api/crunchyroll/name?name={name}");
            return anime;
        }

        public async Task<List<Anime>> GetAnimeByGenre(string genre)
        {
            var animes = await _http.GetFromJsonAsync<List<Anime>>($"api/crunchyroll/name?genre={genre}");
            return animes;
        }

        public async Task<List<Anime>> GetAnimeByRating(double rating)
        {
            var animes = await _http.GetFromJsonAsync<List<Anime>>($"api/crunchyroll/name?rating={rating}");
            return animes;
        }
    }
}
