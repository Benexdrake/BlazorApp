using BlazorApp1.Shared.Models.Crunchyroll;

namespace BlazorApp1.Client.Services
{
    public interface ICrunchyrollService
    {
        Task<List<Anime>> GetAllAnimes();
        Task<List<Anime>> GetAllAnimes(int max);
        Task<List<Anime>> GetAnimeByGenre(string genre);
        Task<Anime> GetAnimeByName(string name);
        Task<List<Anime>> GetAnimeByRating(double rating);
    }
}