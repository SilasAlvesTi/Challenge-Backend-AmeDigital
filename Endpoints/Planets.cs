using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planetas_StarWars.Data;
using Planetas_StarWars.Models;
using Planetas_StarWars.Services;
using Planetas_StarWars.Services.Exceptions;

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

            planets.MapPost("/", async (PlanetsContext context, PlanetInsert planetInsert, MovieAppearanceService movieAppearanceService) => {
                Planet planet = new Planet(Guid.NewGuid(), planetInsert.Name, planetInsert.Wheater, planetInsert.Terrain);
                try
                {
                    var totalAppearances = await movieAppearanceService.GetMovieAppearanceTimesAsync(planetInsert.Name);
                    planet.AddMovieAppearances(totalAppearances);
                }
                catch (APIRequestException e)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = 400,
                        Title = "An error occurred while processing your request.",
                        Detail = e.Message
                    };
                    return Results.BadRequest(problemDetails);
                }
                await context.Planets.AddAsync(planet);
                await context.SaveChangesAsync();
                return Results.Created($"/{planet.Id}", planet);
            });

            planets.MapGet("/{id:Guid}", async (PlanetsContext context, Guid id) =>
            {
                var planet = await context.Planets.FirstAsync();
                
                if (planet == null)
                {
                    return Results.NotFound();
                }
                
                return Results.Ok(planet);
            });

            planets.MapGet("/{name}", async (PlanetsContext context, string name) =>
            {
                var planet = await context.Planets.Where((Planet x) => x.Name == name).FirstAsync();
                
                if (planet == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(planet);
            });

            planets.MapDelete("/{id:guid}", async (PlanetsContext context, Guid id) => {
                var planet = await context.Planets.FindAsync(id);

                if (planet == null)
                {
                    return Results.NotFound();
                }

                context.Planets.Remove(planet);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}