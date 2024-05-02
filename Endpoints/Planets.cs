using Microsoft.EntityFrameworkCore;
using Planetas_StarWars.Data;
using Planetas_StarWars.Models;

namespace Planetas_StarWars.Endpoints
{
    public static class Planets
    {
        public static void RegisterPlanetsEndpoints(this IEndpointRouteBuilder routes)
        {
            var planets = routes.MapGroup("/planets");

            planets.MapGet("/", async (PlanetsContext context) => 
            {
                return await context.Planets.ToListAsync();
            });

            planets.MapPost("/", async (PlanetsContext context, Planet planet) => {
                await context.Planets.AddAsync(planet);
                await context.SaveChangesAsync();
                return Results.Created($"/{planet.Id}", planet);
            });

            planets.MapGet("/{id}", async (PlanetsContext context, Guid id) =>
            {
                return await context.Planets.FindAsync(id);
            });
        }
    }
}