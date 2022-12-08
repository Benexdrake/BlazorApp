
using BlazorApp1.Shared.Models.Crunchyroll;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Data
{
    public class CrunchyrollDBContext : DbContext
    {
        public DbSet<Anime> Animes { get; set; }

        public CrunchyrollDBContext(DbContextOptions<CrunchyrollDBContext> options) : base(options)
        {

        }
    }
}
