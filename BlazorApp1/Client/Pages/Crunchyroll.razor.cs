using BlazorApp1.Shared.Models.Crunchyroll;

namespace BlazorApp1.Client.Pages
{
    public partial class Crunchyroll
    {
        public List<Anime> Animes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Animes = await cs.GetAllAnimes(20);
        }
    }
}