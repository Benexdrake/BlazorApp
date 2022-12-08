using BlazorApp1.Shared.Models;

namespace BlazorApp1.Client.Services
{
    public interface IDiscordServices
    {
        Task<User> GetMyUserAsync();
        Task<User> GetUserAsync(string userId);
        Task<string> GetUserID();
    }
}