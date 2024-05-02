using Microsoft.EntityFrameworkCore;
using Planetas_StarWars.Models;

namespace Planetas_StarWars.Data
{
    public class PlanetsContext : DbContext
    {
        public PlanetsContext (DbContextOptions<PlanetsContext> options)
            : base(options)
        {
        }

        public DbSet<Planet> Planets { get; set; } = null!;
    }
}