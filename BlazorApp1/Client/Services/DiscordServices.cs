using BlazorApp1.Shared.Models;
using Discord;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services
{
    public class DiscordServices : IDiscordServices
    {
        private readonly HttpClient _http;

        public DiscordServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var user = await _http.GetFromJsonAsync<User>($"/user/getuser?userid={userId}");
            return user;
        }

        public async Task<User> GetMyUserAsync()
        {
            var user = await _http.GetFromJsonAsync<User>("/user/getuser");
            return user;
        }

        public async Task<string> GetUserID()
        {
            Console.WriteLine("Hi");
            var id = await _http.GetFromJsonAsync<Object>("/user/getuserid");
            return id.ToString();
        }
    }
}
