
using BlazorApp1.Server.Data;
using BlazorApp1.Shared.Models.Crunchyroll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("api/crunchyroll")]
    public class CrunchyrollAnimeController : ControllerBase
    {
        private readonly IServiceProvider _service;
        private readonly CrunchyrollDBContext _context;

        public CrunchyrollAnimeController(IServiceProvider service, CrunchyrollDBContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll(int max)
        {
            List<Anime> animes = new();
            if (max > 0)
            {
                animes = _context.Animes.Take(max).ToList();
            }
            else
            {
                animes = _context.Animes.ToList();
            }
            if (animes is not null || animes.Count > 0)
                return Ok(animes);
            return BadRequest();
        }

        [HttpGet("name")]
        public async Task<ActionResult> GetByName(string name)
        {
            var anime = _context.Animes.Where(x => x.Name.Contains(name)).FirstOrDefault();

            if (anime is not null)
                return Ok(anime);
            return BadRequest();
        }

        [HttpGet("genre")]
        public async Task<ActionResult> GetByGenre(string genre)
        {
            var animes = _context.Animes.Where(x => x.Tags.Contains(genre)).ToList();
            if (animes is not null || animes.Count > 0)
                return Ok(animes);
            return BadRequest();
        }

        [HttpGet("rating")]
        public async Task<ActionResult> GetByRating(double rating)
        {
            //var animes = _context.Animes.Where(x => double.Parse(x.Rating.Replace(".", ",")) >= rating).ToList();

            var Animes = new List<Anime>();

            foreach (var anime in _context.Animes.ToList())
            {
                var isNumber = double.TryParse(anime.Rating.Replace(".", ","), out double result);

                if (isNumber)
                {
                    if (result >= rating)
                    {
                        Animes.Add(anime);
                    }
                }
            }

            if (Animes is not null || Animes.Count > 0)
                return Ok(Animes);
            return BadRequest();
        }
    }
}
